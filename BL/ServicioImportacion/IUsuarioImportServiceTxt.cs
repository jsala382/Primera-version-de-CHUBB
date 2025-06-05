using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ServicioImportacion
{
    public interface IUsuarioImportServiceTxt
    {
        List<string> ImportUsuarioDesdeTxt(Stream fileStream );
    }
}
