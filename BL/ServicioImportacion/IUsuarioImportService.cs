using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ServicioImportacion
{
    public interface IUsuarioImportService
    {
        List<string> ImportUsuarioDesdeTxt(Stream fileStream );
    }
}
