using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VATAduana
{
    public class LiqGranos
    {
        wslpg.LpgService objWSLPG = new wslpg.LpgService();
        wslpg.LpgAuthType objAuth = new wslpg.LpgAuthType();
        public Dictionary<string, string> Codigos { get; set; }
        public Dictionary<string, string> Puertos { get; set; }
        public Dictionary<string, string> Actividades { get; set; }

        /// <summary>
        /// Retorna un diccionario con los tipos de operacion por actividades
        /// </summary>
        /// <param name="auth">objeto de tipo autenticacion</param>
        /// <returns></returns>
        public List<string> consultarOperacionAct(wslpg.LpgAuthType Auth, long act)
        {
            List<string> dictActividad = new List<string>();
            wslpg.LpgTipoOperacionReturnType objTipoActividad = new wslpg.LpgTipoOperacionReturnType();

            try
            {
                objTipoActividad = objWSLPG.tipoOperacionXActividadConsultar(Auth, act);
                foreach (var item in objTipoActividad.tiposOperacion)
                {
                    dictActividad.Add(item.descripcion);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dictActividad;
        }

        /// <summary>
        /// Retorna un diccionario con los tipos de codigo de Actividades
        /// </summary>
        /// <param name="auth">objeto de tipo autenticacion</param>
        /// <returns></returns>
        public void consultarActividades(string toke, string sign, long cuit)
        {
            Dictionary<string, string> dictActividad = new Dictionary<string, string>();
            wslpg.LpgTipoActividadReturnType objTipoActividad = new wslpg.LpgTipoActividadReturnType();
            objAuth.token = toke;
            objAuth.sign = sign;
            objAuth.cuit = cuit;

            try
            {
                objTipoActividad = objWSLPG.tipoActividadConsultar(objAuth);
                foreach (var item in objTipoActividad.tiposActividad)
                {
                    dictActividad.Add(item.codigo, item.descripcion);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            this.Actividades = dictActividad;
        }

        /// <summary>
        /// Retorna un diccionario con los tipos de codigo de Puertos
        /// </summary>
        /// <param name="auth">objeto de tipo autenticacion</param>
        /// <returns></returns>
        public void consultarPuertos(string toke, string sign, long cuit)
        {
            Dictionary<string, string> dictPuertos = new Dictionary<string, string>();
            wslpg.LpgPuertoReturnType objTipoPuerto = new wslpg.LpgPuertoReturnType();
            objAuth.token = toke;
            objAuth.sign = sign;
            objAuth.cuit = cuit;

            try
            {
                objTipoPuerto = objWSLPG.puertoConsultar(objAuth);
                foreach (var item in objTipoPuerto.puertos)
                {
                    dictPuertos.Add(item.codigo, item.descripcion);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            this.Puertos = dictPuertos;
        }


        /// <summary>
        /// Retorna un diccionario con los tipos de codigo de granos
        /// </summary>
        /// <param name="auth">objeto de tipo autenticacion</param>
        /// <returns></returns>
        public void consultarGranos(string toke, string sign, long cuit)
        {
            Dictionary<string, string> dictCodigos = new Dictionary<string, string>();
            wslpg.LpgTipoGranoReturnType objTipoGrano = new wslpg.LpgTipoGranoReturnType();
            objAuth.token = toke;
            objAuth.sign = sign;
            objAuth.cuit = cuit;

            try
            {
                objTipoGrano = objWSLPG.tipoGranoConsultar(objAuth);
                foreach (var item in objTipoGrano.granos)
                {
                    dictCodigos.Add(item.codigo, item.descripcion);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            this.Codigos = dictCodigos;
        }
        
        /// <summary>
        /// Verifica que los servidores de LPG esten activos, appServer = OK,  authServer = OK, dbServer = OK
        /// </summary>
        /// <returns></returns>
        public bool dummyServers()
        {
            bool todoOk = false; 
            wslpg.dummyRespType objDummy = new wslpg.dummyRespType();
            try
            {
                objDummy = objWSLPG.dummy();              
                if (objDummy.@return.appserver == "OK" && objDummy.@return.authserver == "OK" && objDummy.@return.dbserver == "OK")
                {
                    Console.WriteLine("Servicio Web Activo (Granos)");
                    todoOk = true;
                }
            }
            catch (Exception excepcionAlInvocarDummy)
            {
                throw new Exception("***Error INVOCANDO al Dummy WSLPG : " + excepcionAlInvocarDummy.Message);
            }
            return todoOk;
        }

        /// <summary>
        /// Consulta una liquidacion primaria de granos por coe
        /// </summary>
        /// <param name="token">Token de autenticacion generado por la wsaa</param>
        /// <param name="sign">Sign de autenticacion generado por la wsaa</param>
        /// <param name="cuit">Cuit de la compañia</param>
        /// <param name="coe">identificador del coe</param>
        /// <returns></returns>liqConsXCoeResp
        public wslpg.LpgLiqConsReturnType consultarLiqXCoe(string token, string sign, long cuit, long coe)
        {
            objAuth.token = token;
            objAuth.sign = sign;
            objAuth.cuit = cuit;
            
            wslpg.LpgLiqConsReturnType _LiqXCoe = new wslpg.LpgLiqConsReturnType();

            try
            {
                _LiqXCoe = objWSLPG.liquidacionXCoeConsultar(objAuth, coe, wslpg.LpgSiNoType.N, false);
            }
            catch (Exception consultarLiqPrimException)
            {
                throw new Exception("***Error INVOCANDO Consulta liquidacion primaria: " + consultarLiqPrimException.Message);
            }


            if (_LiqXCoe.errores == null)
            {
                List<string> dictOperAct = consultarOperacionAct(objAuth, Convert.ToUInt32(_LiqXCoe.liquidacion.nroActComprador));
                foreach (var item in dictOperAct)
                {
                    _LiqXCoe.autorizacion.codTipoOperacion += string.Format(@" - {0}", item);
                }
                return _LiqXCoe;
            }
            else
            {
                throw new Exception($"Error {_LiqXCoe.errores[0].codigo}: {_LiqXCoe.errores[0].descripcion}");
            }

        }

        /// <summary>
        /// Consultar un ajuste por coe
        /// </summary>
        /// <param name="token">Token de autenticacion generado por la wsaa</param>
        /// <param name="sign">Sign de autenticacion generado por la wsaa</param>
        /// <param name="cuit">Cuit de la compañia</param>
        /// <param name="coe">identificador del coe</param>
        /// <returns></returns>
        public wslpg.LpgAjusteConsReturnType consultarAjusXCoe(string token, string sign, long cuit, long coe)
        {
            objAuth.token = token;
            objAuth.sign = sign;
            objAuth.cuit = cuit;

            wslpg.LpgAjusteConsReturnType _AjusXCoe = new wslpg.LpgAjusteConsReturnType();

            try
            {
                _AjusXCoe = objWSLPG.ajusteXCoeConsultar(objAuth, coe, wslpg.LpgSiNoType.N, false);
            }
            catch (Exception consultarAjusException)
            {
                throw new Exception("***Error INVOCANDO Consulta ajuste: " + consultarAjusException.Message);
            }

            if (_AjusXCoe.errores == null)
            {
                return _AjusXCoe;
            }
            else
            {
                throw new Exception($"Error {_AjusXCoe.errores[0].codigo}: {_AjusXCoe.errores[0].descripcion}");
            }
        }

        /// <summary>
        /// Consultar una liquidacion secundaria de granos por coe
        /// </summary>
        /// <param name="token">Token de autenticacion generado por la wsaa</param>
        /// <param name="sign">Sign de autenticacion generado por la wsaa</param>
        /// <param name="cuit">Cuit de la compañia</param>
        /// <param name="coe">identificador del coe</param>
        /// <returns></returns>
public wslpg.LsgConsultaReturnType consultarLsgXCoe(string token, string sign, long cuit, long coe)
        {
            objAuth.token = token;
            objAuth.sign = sign;
            objAuth.cuit = cuit;

            wslpg.LsgConsultaReturnType _LsgXCoe = new wslpg.LsgConsultaReturnType();
            try
            {
                _LsgXCoe = objWSLPG.lsgConsultarXCoe(objAuth, coe, wslpg.LpgSiNoType.N, false);
            }
            catch (Exception consultarSecunException)
            {
                throw new Exception("***Error INVOCANDO Consulta liquidacion secundaria: " + consultarSecunException.Message);
            }

            if (_LsgXCoe.errores == null)
            {
                return _LsgXCoe;
            }
            else
            {
                throw new Exception($"Error {_LsgXCoe.errores[0].codigo}: {_LsgXCoe.errores[0].descripcion}");
            }
        }
    }
}
