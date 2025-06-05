using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ServicioImportacion
{
    public interface IUsuarioImportServiceXls
    {
        List<string> ImportUserDesdeXls(Stream fileStream);
    }
}
