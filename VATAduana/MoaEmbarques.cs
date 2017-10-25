using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATAduana
{
    class MoaEmbarques
    {
        //Variables globales de la clase, seteamos los tipo de datos.
        wconsdeclaracion.wconsdeclaracion objWCONSDECLA = new wconsdeclaracion.wconsdeclaracion();
        wconsdeclaracion.WSAutenticacionEmpresa objAuth = new wconsdeclaracion.WSAutenticacionEmpresa();

        /// <summary>
        /// Permite verificar el estado de los tres servidores, de la aplicacion, de autenticacion y de base de datos.
        /// No recibe ningun parametro
        /// </summary>
        /// <returns></returns>
        public bool dummyServers()
        {   
            //Variable para retornar
            bool todoOk = false;
            //Set variaable de resultado dummy
            wconsdeclaracion.ResultadoEjecucionDummy objDummy = new wconsdeclaracion.ResultadoEjecucionDummy();
            //Invoco el metodo dummy
            try
            {
                objDummy = objWCONSDECLA.Dummy();
            }
            catch (Exception excepcionAlInvocarDummy)
            {
                throw new Exception("***Error INVOCANDO al Dummy WCONSDECLARACION: " + excepcionAlInvocarDummy.Message);
            }
            //Si no hay excepciones, verifico que todos los servidores estan ok
            if (objDummy.AppServer == "OK" && objDummy.AuthServer == "OK" && objDummy.DbServer == "OK")
            {
                Console.WriteLine("Servicio Web Activo (MOA)");
                todoOk = true;
            }
            //Retorno variable, False = problemas en al menos un servidor; True = Los tres servidores estan "OK"
            return todoOk;
        }

        /// <summary>
        /// Permite consultar la lista entera de declaraciones en un rango de fechas no maximo a 30 dias.
        /// </summary>
        /// <param name="token">Token de autenticacion</param>
        /// <param name="sign">Sign de autenticacion</param>
        /// <param name="cuit">Cuit del importador/exportador</param>
        /// <param name="fechaInicio">Fecha desde el cual quiero buscar la lista de declaraciones</param>
        /// <param name="fechaFin">Fecha hasta donde quiero buscar la lista de declaraciones, maximo 30 dias</param>
        /// <returns></returns>
        public List<string> consultarDetalle(string token, string sign, string cuit, DateTime fechaInicio, DateTime fechaFin, string tipoOp)
        {
            //Variables locales
            List<string> listaIdentificadores = new List<string>();
            wconsdeclaracion.DetalladaListaDeclaracionesParams objDetalleParametros = new wconsdeclaracion.DetalladaListaDeclaracionesParams();
            wconsdeclaracion.DetalladaListaDeclaracionesRta objConsultaDetalladaListaRta = new wconsdeclaracion.DetalladaListaDeclaracionesRta();
            //Completo parametro de tipo Auth
            objAuth.Token = token;
            objAuth.Sign = sign;
            objAuth.CuitEmpresaConectada = cuit;
            objAuth.TipoAgente = "IMEX";
            objAuth.Rol = "IMEX";
            //Completo parametro de tipo DetalleParametros
            objDetalleParametros.CodigoTipoOperacion = tipoOp;
            objDetalleParametros.CuitImportadorExportador = cuit;
            objDetalleParametros.FechaOficializacionDesde = Convert.ToDateTime("2017-07-31T00:00:00-03:00"); //fechaInicio
            objDetalleParametros.FechaOficializacionHasta = Convert.ToDateTime("2017-08-20T00:00:00-03:00");//fechaFin;
            //Invoco el WS DetalladaListaDeclaraciones, si algo ocurre invocando el servicio lanzo una excepcion.
            try
            {
                objConsultaDetalladaListaRta = objWCONSDECLA.DetalladaListaDeclaraciones(objAuth, objDetalleParametros);                
            }
            catch (Exception excepcionAlInvocarDetalladaListaDeclaraciones)
            {
                throw new Exception("***Error INVOCANDO Lista de Declaraciones: " + excepcionAlInvocarDetalladaListaDeclaraciones.Message);
            }
            //Analizo la lista de errores
            //Si el codigo es 0 = Ejecucion exitosa, retorno lista de declaraciones (solo identificador)
            //Si es codigo cero pero la lista de declaraciones esta vacia, lanzo excepcion
            //Si existe otro error, lanzo excepcion con el error
            if (objConsultaDetalladaListaRta.ListaErrores[0].Codigo == 0)
            {
                if (objConsultaDetalladaListaRta.Declaraciones != null)
                {
                    try
                    {
                        //Recorro objeto respuesta de la invocacion del servicio y lleno lista con solo identificadores para retornar
                        foreach (wconsdeclaracion.DeclaracionDetalladaMOAListaDeclaraciones item in objConsultaDetalladaListaRta.Declaraciones)
                        {
                            listaIdentificadores.Add(item.IdentificadorExterno);
                        }
                    }
                    catch (Exception ExcepcionAlLlenarLista)
                    {
                        throw new Exception("***Error llenando lista con declaraciones: " + ExcepcionAlLlenarLista.Message);
                    }
                    return listaIdentificadores;
                }
                else
                {
                    throw new Exception("No Existe declaraciones para la fecha solicitada");
                }                
            }
            else
            {
                throw new Exception("***Error " + objConsultaDetalladaListaRta.ListaErrores[0].Codigo + ": " + objConsultaDetalladaListaRta.ListaErrores[0].Descripcion);
            }
        }

        /// <summary>
        /// Permite consultar la caratula de una declaracion
        /// </summary>
        /// <param name="token">Token de autenticacion</param>
        /// <param name="sign">Sign de autenticacion</param>
        /// <param name="cuit">Cuit del importador/exportador</param>
        /// <param name="argIdentificador">Identificador de la declaracion</param>
        /// <returns>Retorna objeto de tipo caratula</returns>
        public wconsdeclaracion.DeclaracionDetalladaMOACaratula consultarCaratula(string token, string sign, string cuit, string argIdentificador)
        {
            wconsdeclaracion.DetalladaCaratulaRta objConsultaDetalladaCaratulaRta = new wconsdeclaracion.DetalladaCaratulaRta();
            //Completo parametro de tipo Auth
            objAuth.Token = token;
            objAuth.Sign = sign;
            objAuth.CuitEmpresaConectada = cuit;
            objAuth.TipoAgente = "IMEX";
            objAuth.Rol = "IMEX";
            //Completo parametro de tipo DetalleParametros
            string argIdentificadorDestinacion = argIdentificador;
            //Invoco el WS DetalladaCaratula
            try
            {
                objConsultaDetalladaCaratulaRta = objWCONSDECLA.DetalladaCaratula(objAuth, argIdentificadorDestinacion);
            }
            catch (Exception excepcionAlInvocarDetalleCaratula)
            {
                throw new Exception("***Error INVOCANDO Detalle caratula: " + excepcionAlInvocarDetalleCaratula.Message);
            }
            //Analizo lista de errores
            //Si todo sale bien retorno caratula sino lanzo excepcion
            if(objConsultaDetalladaCaratulaRta.ListaErrores[0].Codigo == 0)
            {
                return objConsultaDetalladaCaratulaRta.Caratula;
            }
            else
            {
                //Puede que se convierta en log.add(error)
                throw new Exception("***Error " + objConsultaDetalladaCaratulaRta.ListaErrores[0].Codigo + ": " + objConsultaDetalladaCaratulaRta.ListaErrores[0].Descripcion);
            }            
        }

        /// <summary>
        /// Permite consultar el estado de una declaracion
        /// </summary>
        /// <param name="token">Token de autenticacion</param>
        /// <param name="sign">Sign de autenticacion</param>
        /// <param name="cuit">Cuit del importador/exportador</param>
        /// <param name="argIdentificador">Identificador de la declaracion</param>
        /// <returns>Retorna objeto de tipo estado</returns>
        public wconsdeclaracion.DeclaracionDetalladaMOAEstado consultarEstado(string token, string sign, string cuit, string argIdentificador)
        {
            wconsdeclaracion.DetalladaEstadoRta objConsultaDetalladaEstadoRta = new wconsdeclaracion.DetalladaEstadoRta();
            //Completo parametro de tipo Auth
            objAuth.Token = token;
            objAuth.Sign = sign;
            objAuth.CuitEmpresaConectada = cuit;
            objAuth.TipoAgente = "IMEX";
            objAuth.Rol = "IMEX";
            //Completo parametro de tipo DetalleParametros
            string argIdentificadorDestinacion = argIdentificador;
            //Invoco el WS DetalladaEstado
            try
            {
                objConsultaDetalladaEstadoRta = objWCONSDECLA.DetalladaEstado(objAuth, argIdentificadorDestinacion);
            }
            catch (Exception excepcionAlInvocarDetalleEstado)
            {
                throw new Exception("***Error INVOCANDO Detalle estado: " + excepcionAlInvocarDetalleEstado.Message);
            }
            //Analizo lista de errores
            //Si todo sale bien retorno estado sino lanzo excepcion
            if (objConsultaDetalladaEstadoRta.ListaErrores[0].Codigo == 0)
            {
                return objConsultaDetalladaEstadoRta.Estado;
            }
            else
            {
                //Puede que se convierta en log.add(error)
                throw new Exception("***Error " + objConsultaDetalladaEstadoRta.ListaErrores[0].Codigo + ": " + objConsultaDetalladaEstadoRta.ListaErrores[0].Descripcion);
            }
        }

        /// <summary>
        /// Permite consultar la lista de items de una declaracion
        /// </summary>
        /// <param name="token">Token de autenticacion</param>
        /// <param name="sign">Sign de autenticacion</param>
        /// <param name="cuit">Cuit del importador/exportador</param>
        /// <param name="argIdentificador">Identificador de la declaracion</param>
        /// <returns>Retorna objeto de tipo lista item con la info de los items</returns>
        public wconsdeclaracion.DetalladaItemsRta consultarItems(string token, string sign, string cuit, string argIdentificador)
        {
            wconsdeclaracion.DetalladaItemsRta objConsultaDetalladaItemsRta = new wconsdeclaracion.DetalladaItemsRta();
            //Completo parametro de tipo Auth
            objAuth.Token = token;
            objAuth.Sign = sign;
            objAuth.CuitEmpresaConectada = cuit;
            objAuth.TipoAgente = "IMEX";
            objAuth.Rol = "IMEX";
            //Completo parametro de tipo DetalleParametros
            string argIdentificadorDestinacion = argIdentificador;
            //Invoco el WS DetalladaItems
            try
            {
                objConsultaDetalladaItemsRta = objWCONSDECLA.DetalladaItems(objAuth, argIdentificadorDestinacion);
            }
            catch (Exception excepcionAlInvocarItemsEstado)
            {
                throw new Exception("***Error INVOCANDO Items estado: " + excepcionAlInvocarItemsEstado.Message);
            }
            //Analizo lista de errores
            //Si todo sale bien retorno lista items sino lanzo excepcion
            if (objConsultaDetalladaItemsRta.ListaErrores[0].Codigo == 0)
            {
                return objConsultaDetalladaItemsRta;
            }
            else
            {
                //Puede que se convierta en log.add(error)
                throw new Exception("***Error " + objConsultaDetalladaItemsRta.ListaErrores[0].Codigo + ": " + objConsultaDetalladaItemsRta.ListaErrores[0].Descripcion);
            }
        }

        /// <summary>
        /// Permite consultar la lista de subitems de una declaracion
        /// </summary>
        /// <param name="token">Token de autenticacion</param>
        /// <param name="sign">Sign de autenticacion</param>
        /// <param name="cuit">Cuit del importador/exportador</param>
        /// <param name="argIdentificador">Identificador de la declaracion</param>
        /// <param name="argIdentificadorItem">Identificador del Item</param>
        /// <returns>Retorna objeto de tipo lista item con la info de los items</returns>
        public wconsdeclaracion.DetalladaSubitemRta consultarSubItems(string token, string sign, string cuit, string argIdentificador, string argIdentificadorItem)
        {
            wconsdeclaracion.DetalladaSubitemRta objConsultaDetalladaSubItemsRta = new wconsdeclaracion.DetalladaSubitemRta();
            wconsdeclaracion.DetalladaSubitemsParams objDetalladaSubItemsParams = new wconsdeclaracion.DetalladaSubitemsParams();
            //Completo parametro de tipo Auth
            objAuth.Token = token;
            objAuth.Sign = sign;
            objAuth.CuitEmpresaConectada = cuit;
            objAuth.TipoAgente = "IMEX";
            objAuth.Rol = "IMEX";
            //Completo parametro de tipo DetalleSubItemsParametros
            objDetalladaSubItemsParams.IdentificadorDestinacion = argIdentificador;
            objDetalladaSubItemsParams.IdentificadorItem = argIdentificadorItem;
            //Invoco el WS DetalladaSubItem
            try
            {
                objConsultaDetalladaSubItemsRta = objWCONSDECLA.DetalladaSubitem(objAuth, objDetalladaSubItemsParams);
            }
            catch (Exception excepcionAlInvocarItemsEstado)
            {
                throw new Exception("***Error INVOCANDO Sub Items: " + excepcionAlInvocarItemsEstado.Message);
            }
            //Analizo lista de errores
            //Si todo sale bien retorno listasubitems sino lanzo excepcion
            if (objConsultaDetalladaSubItemsRta.ListaErrores[0].Codigo == 0)
            {
                return objConsultaDetalladaSubItemsRta;
            }
            else
            {
                //Puede que se convierta en log.add(error)
                throw new Exception("***Error " + objConsultaDetalladaSubItemsRta.ListaErrores[0].Codigo + ": " + objConsultaDetalladaSubItemsRta.ListaErrores[0].Descripcion);
            }
        }

        /// <summary>
        /// Permite consultar otros datos de una declaracion
        /// </summary>
        /// <param name="token">Token de autenticacion</param>
        /// <param name="sign">Sign de autenticacion</param>
        /// <param name="cuit">Cuit del importador/exportador</param>
        /// <param name="argIdentificador">Identificador de la declaracion</param>
        /// <returns>Retorna objeto de tipo lista item con la info de los items</returns>
        public wconsdeclaracion.DetalladaOtrosDatosRta consultarOtrosDatos(string token, string sign, string cuit, string argIdentificador,string argIdentificadorItem)
        {
            wconsdeclaracion.DetalladaOtrosDatosRta objConsultaDetalladaOtrosDatosRta = new wconsdeclaracion.DetalladaOtrosDatosRta();
            wconsdeclaracion.DetalladaOtrosDatosParams objDetalladaOtrosDatosParams = new wconsdeclaracion.DetalladaOtrosDatosParams();

            //Completo parametro de tipo Auth
            objAuth.Token = token;
            objAuth.Sign = sign;
            objAuth.CuitEmpresaConectada = cuit;
            objAuth.TipoAgente = "IMEX";
            objAuth.Rol = "IMEX";
            //Completo parametro de tipo DetalleSubItemsParametros
            objDetalladaOtrosDatosParams.IdentificadorDestinacion = argIdentificador;
            objDetalladaOtrosDatosParams.IdentificadorItem = Convert.ToInt32(argIdentificadorItem);
            //Invoco el WS DetalladaSubItem
            try
            {
                objConsultaDetalladaOtrosDatosRta = objWCONSDECLA.DetalladaOtrosDatos(objAuth, objDetalladaOtrosDatosParams);
            }
            catch (Exception excepcionAlInvocarItemsEstado)
            {
                throw new Exception("***Error INVOCANDO Otros Datos: " + excepcionAlInvocarItemsEstado.Message);
            }
            //Analizo lista de errores
            //Si todo sale bien retorno otros datos sino lanzo excepcion
            if (objConsultaDetalladaOtrosDatosRta.ListaErrores[0].Codigo == 0)
            {
                return objConsultaDetalladaOtrosDatosRta;
            }
            else
            {
                //Puede que se convierta en log.add(error)
                throw new Exception("***Error " + objConsultaDetalladaOtrosDatosRta.ListaErrores[0].Codigo + ": " + objConsultaDetalladaOtrosDatosRta.ListaErrores[0].Descripcion);
            }
        }

        /// <summary>
        /// Permite consultar documentos de una declaracion
        /// </summary>
        /// <param name="token">Token de autenticacion</param>
        /// <param name="sign">Sign de autenticacion</param>
        /// <param name="cuit">Cuit del importador/exportador</param>
        /// <param name="argIdentificador">Identificador de la declaracion</param>
        /// <returns>Retorna objeto de tipo lista item con la info de los items</returns>
        public wconsdeclaracion.DetalladaDocumentosRta consultarDocumentos(string token, string sign, string cuit, string argIdentificador)
        {
            wconsdeclaracion.DetalladaDocumentosRta objConsultaDetalladaDocumentos = new wconsdeclaracion.DetalladaDocumentosRta();         

            //Completo parametro de tipo Auth
            objAuth.Token = token;
            objAuth.Sign = sign;
            objAuth.CuitEmpresaConectada = cuit;
            objAuth.TipoAgente = "IMEX";
            objAuth.Rol = "IMEX";
            //Invoco el WS DetalladaSubItem
            try
            {
                objConsultaDetalladaDocumentos = objWCONSDECLA.DetalladaDocumentos(objAuth, argIdentificador);
            }
            catch (Exception excepcionAlInvocarItemsEstado)
            {
                throw new Exception("***Error INVOCANDO Documentos Destinacion: " + excepcionAlInvocarItemsEstado.Message);
            }
            //Analizo lista de errores
            //Si todo sale bien retorno otros datos sino lanzo excepcion
            if (objConsultaDetalladaDocumentos.ListaErrores[0].Codigo == 0)
            {
                return objConsultaDetalladaDocumentos;
            }
            else
            {
                //Puede que se convierta en log.add(error)
                throw new Exception("***Error " + objConsultaDetalladaDocumentos.ListaErrores[0].Codigo + ": " + objConsultaDetalladaDocumentos.ListaErrores[0].Descripcion);
            }
        }
    }

}
