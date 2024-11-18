using POEPart1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POEPart1.DataStructures
{
    public class ServiceRequestBST
    {
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// TreeNode that holds the root of the BST
        /// </summary>
        private TreeNode root;

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to insert a service request into the BST
        /// </summary>
        /// <param name="request"></param>
        public bool Insert(ServiceRequest request)
        {
            if (request == null || Search(request.ServiceRequestID) != null)
            {
                return false;
            }
            root = InsertNode(root, request);
            return true;
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to insert a node into the BST
        /// </summary>
        /// <param name="node"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private TreeNode InsertNode(TreeNode node, ServiceRequest request)
        {
            if (node == null)
                return new TreeNode(request);

            if (request.ServiceRequestID < node.Data.ServiceRequestID)
                node.Left = InsertNode(node.Left, request);
            else if (request.ServiceRequestID > node.Data.ServiceRequestID)
                node.Right = InsertNode(node.Right, request);

            return node;
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to search for a service request by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceRequest Search(int id)
        {
            if (id <= 0)
            {
                Console.WriteLine("Error: Invalid service request ID.");
                return null;
            }
            return SearchNode(root, id)?.Data;
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to search for a node in the BST
        /// </summary>
        /// <param name="node"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private TreeNode SearchNode(TreeNode node, int id)
        {
            if (node == null)
                return null;

            if (id == node.Data.ServiceRequestID)
                return node;
            else if (id < node.Data.ServiceRequestID)
                return SearchNode(node.Left, id);
            else
                return SearchNode(node.Right, id);
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to display all requests (in-order traversal)
        /// </summary>
        /// <returns></returns>
        public List<ServiceRequest> DisplayAllRequests()
        {
            if (root == null)
            {
                Console.WriteLine("No service requests available.");
                return new List<ServiceRequest> { new ServiceRequest { Title = "No service requests found." } };
            }
            List<ServiceRequest> requests = InOrderTraversal(root);
            return requests;
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to perform an in-order traversal of the BST
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<ServiceRequest> InOrderTraversal(TreeNode node)
        {
            List<ServiceRequest> requests = new List<ServiceRequest>();

            if (node != null)
            {
                requests.AddRange(InOrderTraversal(node.Left));
                requests.Add(node.Data);
                requests.AddRange(InOrderTraversal(node.Right));
            }

            return requests;
        }
        //-----------------------------------------------------------------------------------------------//
    }
}

//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//
