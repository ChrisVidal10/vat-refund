using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATAduana
{
    class LiqGranos
    {
        wslpg.LpgService objWSLPG = new wslpg.LpgService();
        wslpg.LpgAuthType objAuth = new wslpg.LpgAuthType();
        
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
        /// <returns></returns>
        public wslpg.LpgLiquidacionBaseType consultarLiqXCoe(string token, string sign, long cuit, long coe)
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

            if (_LiqXCoe.errores[0].codigo == "0")
            {
                return _LiqXCoe.liquidacion;
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
        public wslpg.LpgAjusteUnificadoRespType consultarAjusXCoe(string token, string sign, long cuit, long coe)
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

            if (_AjusXCoe.errores[0].codigo == "0")
            {
                return _AjusXCoe.ajusteUnificado;
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
        public wslpg.LsgLiquidacionesType consultarLsgXCoe(string token, string sign, long cuit, long coe)
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

            if (_LsgXCoe.errores[0].codigo == "0")
            {
                return _LsgXCoe.liquidaciones[0];
            }
            else
            {
                throw new Exception($"Error {_LsgXCoe.errores[0].codigo}: {_LsgXCoe.errores[0].descripcion}");
            }
        }
    }
}
