using POEPart1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POEPart1.DataStructures
{
    public class ServiceRequestHeap
    {
        /// <summary>
        /// List that holds the heap
        /// </summary>
        private List<(ServiceRequest request, int dependencyCount)> heap = new List<(ServiceRequest request, int dependencyCount)>();

        /// <summary>
        /// Int that holds the count of elements in the heap
        /// </summary>
        public int Count => heap.Count;

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceRequestHeap()
        {
            heap = new List<(ServiceRequest, int)>();
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to insert a service request into the heap
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dependencyCount"></param>
        public void Insert(ServiceRequest request, int dependencyCount)
        {
            heap.Add((request, dependencyCount));
            HeapifyUp(heap.Count - 1);
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to extract the max element from the heap
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public (ServiceRequest request, int dependencyCount) ExtractMax()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty");

            var max = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);

            return max;
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to heapify up the heap
        /// </summary>
        /// <param name="index"></param>
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (heap[index].dependencyCount <= heap[parentIndex].dependencyCount)
                    break;

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to heapify down the heap
        /// </summary>
        /// <param name="index"></param>
        private void HeapifyDown(int index)
        {
            while (index < heap.Count)
            {
                int leftChildIndex = 2 * index + 1;
                int rightChildIndex = 2 * index + 2;
                int largestIndex = index;

                if (leftChildIndex < heap.Count && heap[leftChildIndex].dependencyCount > heap[largestIndex].dependencyCount)
                {
                    largestIndex = leftChildIndex;
                }

                if (rightChildIndex < heap.Count && heap[rightChildIndex].dependencyCount > heap[largestIndex].dependencyCount)
                {
                    largestIndex = rightChildIndex;
                }

                if (largestIndex == index)
                    break;

                Swap(index, largestIndex);
                index = largestIndex;
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to swap two elements in the heap
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        private void Swap(int index1, int index2)
        {
            var temp = heap[index1];
            heap[index1] = heap[index2];
            heap[index2] = temp;
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//