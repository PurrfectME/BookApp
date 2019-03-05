using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DataAccess
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
