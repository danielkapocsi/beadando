using System;
using System.Collections.Generic;

namespace beadando
{
    public partial class CompProd
    {
        public int Id { get; set; }
        public int ProdId { get; set; }
        public int CompIds { get; set; }

        public CompProd(int id, int prodId, int compIds)
        {
            Id = id;
            ProdId = prodId;
            CompIds = compIds;
        }
    }
}
