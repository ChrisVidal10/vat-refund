using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATAduana
{
    public class Asociacion
    //Clase que nos sirve para asociar destinaciones a items y a subitems
    //Principalmente para la impresion de subitems en el excel mostrando su id de item y de destinacion asociados 
    {
        private string identificadorDestinacion;
        private string identificadorItem;
        private string identificadorSubItem;

        public string IdentificadorDestinacion { get => identificadorDestinacion; set => identificadorDestinacion = value; }
        public string IdentificadorItem { get => identificadorItem; set => identificadorItem = value; }
        public string IdentificadorSubItem { get => identificadorSubItem; set => identificadorSubItem = value; }

        public Asociacion(string idDest, string idItem, string idSubItem)
        //Constructor recibiendo parametros para generar una asociacion especifica
        {
            this.IdentificadorDestinacion = idDest;
            this.IdentificadorItem = idItem;
            this.IdentificadorSubItem = idSubItem;
        }

        public Asociacion()
        //Constructor vacio por si se quiere generar una asociacion sin parametros
        {
        }

        public string devolverIdentificadorItem(List<Asociacion> asociaciones, string idenDest, string sufijoValor)
        //Funcion que recibe una lista de asociaciones y nos devuelte el identificador de item deseado.
        //sufijoValor representa el nombre del subItem. Con el destinacion y el subitem podemos sacar el numero de item.
        {
            string encontrado = "";
            foreach (Asociacion asoc in asociaciones)
            {
                if (asoc.IdentificadorDestinacion == idenDest && asoc.IdentificadorSubItem == sufijoValor)
                //Si lo encuentra lo devuelve
                {
                    encontrado = asoc.IdentificadorItem;
                    return encontrado;
                }
            }
            return encontrado;
            //Si no lo encuentra devuelve vacio
        }

    }
}
