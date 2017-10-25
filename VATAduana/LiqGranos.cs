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

        public object consultarLiqXCoe(string token, string sign, long cuit, long coe)
        {
            objAuth.token = token;
            objAuth.sign = sign;
            objAuth.cuit = cuit;           

            wslpg.LpgLiqConsReturnType _LiqXCoe = new wslpg.LpgLiqConsReturnType();

            _LiqXCoe = objWSLPG.liquidacionXCoeConsultar(objAuth, coe, wslpg.LpgSiNoType.N, false);                     

            return _LiqXCoe;
        }

        public object consultarAjusXCoe(string token, string sign, long cuit, long coe)
        {
            objAuth.token = token;
            objAuth.sign = sign;
            objAuth.cuit = cuit;

            wslpg.LpgAjusteConsReturnType _AjusXCoe = new wslpg.LpgAjusteConsReturnType();

            _AjusXCoe = objWSLPG.ajusteXCoeConsultar(objAuth, coe, wslpg.LpgSiNoType.N, false);

            return _AjusXCoe;
        }

        public object consultarLsgXCoe(string token, string sign, long cuit, long coe)
        {
            objAuth.token = token;
            objAuth.sign = sign;
            objAuth.cuit = cuit;

            wslpg.LsgConsultaReturnType _LsgXCoe = new wslpg.LsgConsultaReturnType();

            _LsgXCoe = objWSLPG.lsgConsultarXCoe(objAuth, coe, wslpg.LpgSiNoType.N, false);

            return _LsgXCoe;
        }
    }
}
