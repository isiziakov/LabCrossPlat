using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Model
{
    public interface IPath
    {
        string GetDatabasePath(string filename);
    }
}
