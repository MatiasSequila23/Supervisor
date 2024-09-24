using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ENTIDAD
{
    public class BalanceoArchivos
    {
        public string Billetes_inicial1;
        public string Billetes_inicial2;
        public string Billetes_dispensado1;
        public string Billetes_dispensado2;


        public BalanceoArchivos(string inicial1,string inicial2,string dispensado1, string dispensado2 )
        {
            

            int i1 = Convert.ToInt32(inicial1);
            int i2 = Convert.ToInt32(inicial2);
            int d1 = Convert.ToInt32(dispensado1);
            int d2 = Convert.ToInt32(dispensado2);


            this.Billetes_inicial1 = i1.ToString("D8");
            this.Billetes_inicial2 = i2.ToString("D8");
            this.Billetes_dispensado1 = d1.ToString("D8");
            this.Billetes_dispensado2 = d2.ToString("D8");

           Archivos.EliminaUltimaLinea(Directory.GetCurrentDirectory() + "\\utils\\balanceo.CSV");


        }

        public string CadenaParaGuardar()
        {
            return this.Billetes_inicial1 + this.Billetes_inicial2 + this.Billetes_dispensado1 + this.Billetes_dispensado2;
        }
    }
}
