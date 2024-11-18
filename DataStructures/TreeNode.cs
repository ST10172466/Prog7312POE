using POEPart1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POEPart1.DataStructures
{
    public class TreeNode
    {
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Data of the node
        /// </summary>
        public ServiceRequest Data { get; set; }

        /// <summary>
        /// Left child of the node
        /// </summary>
        public TreeNode Left { get; set; }

        /// <summary>
        /// Right child of the node
        /// </summary>
        public TreeNode Right { get; set; }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data"></param>
        public TreeNode(ServiceRequest data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//