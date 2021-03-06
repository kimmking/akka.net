﻿using System;
using System.Runtime.Serialization;
using Xunit.Sdk;

namespace Akka.TestKit.Xunit.Internals
{
    public class AkkaEqualException : EqualException
    {
        private readonly string _format;
        private readonly object[] _args;

        public AkkaEqualException(object expected, object actual, string format = "", params object[] args)
            : base(expected, actual)
        {
            _format = format;
            _args = args;
        }

        public AkkaEqualException(object expected, object actual, bool skipPositionCheck, string format = "", params object[] args)
            : base(expected, actual, skipPositionCheck)
        {
            _format = format;
            _args = args;
        }

        protected AkkaEqualException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string Message
        {
            get
            {
                if(string.IsNullOrEmpty(_format))
                    return base.Message;
                string message;
                try
                {
                    message = string.Format(_format, _args);
                }
                catch(Exception)
                {
                    message = "[Could not string.Format(\"" + _format + "\", " + string.Join(", ", _args) + ")]";
                }
                return base.Message + " " + message;
            }
        }
    }
}