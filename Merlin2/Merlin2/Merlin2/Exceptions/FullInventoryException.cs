using System;

namespace Merlin2.Exceptions
{
    internal class FullInventoryException : Exception
    {
        public FullInventoryException() : base()
        {
        }

        public FullInventoryException(string message) : base(message)
        {
        }

        public FullInventoryException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}