using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace AkaneMail
{
  public interface IMailBoxWriter
  {
    void Write(MailBox mailBox, Stream stream);
  }
}
