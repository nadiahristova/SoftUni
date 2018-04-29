namespace GenericList.CustomExceptions
{
    using System;

    class EmptyListException : Exception
    {
        public EmptyListException(string message) :base (message)
        { }
    }
}
