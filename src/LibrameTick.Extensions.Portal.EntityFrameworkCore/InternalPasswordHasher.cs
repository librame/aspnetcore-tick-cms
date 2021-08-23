#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Portal
{
    class InternalPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
            => password.FromEncodingString().AsSha256().AsAes().AsBase64String();

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var hashedBuffer = hashedPassword.FromBase64String().FromAes();
            var providedBuffer = providedPassword.FromEncodingString().AsSha256();

            return hashedBuffer.SequenceEqual(providedBuffer);
        }

    }
}
