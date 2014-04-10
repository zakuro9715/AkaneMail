using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AkaneMail
{
  public class MboxWriter : IMailBoxWriter
  {

    public void Write(MailBox mailBox, Stream stream)
    {
      if (mailBox == null)
        throw new ArgumentNullException("mailBox が null です");
      if (stream == null)
        throw new ArgumentNullException("stream が null です");
      if (!stream.CanWrite)
        throw new ArgumentException("書き込みが許可されていない stream です");

      using (var writer = new StreamWriter(stream))
      {
        foreach (var mail in mailBox.Receive)
          writer.WriteLine(FormatedMail(mail));
      }
    }

    private string FormatedMail(Mail mail)
    {
      var sb = new StringBuilder();
      sb.Append(BuildFromLine(mail));
      using(var reader = new StringReader(BuildFromLine(mail) + mail.header + mail.body))
      {
        string s;
        while((s = reader.ReadLine()) != null)
        sb.Append(FromQuorte(s) + Environment.NewLine);
       } 

      // 末尾に空行を入れて返す
      return sb.ToString() + Environment.NewLine;
    }

    private string BuildFromLine(Mail mail)
    {
      return string.Format("From {0} {1}\n", mail.address, mail.date);
    }

    private string FromQuorte(string text)
    {
      var formatStr = "{0}\n";
      if (System.Text.RegularExpressions.Regex.IsMatch(text, "^ >*From "))
        formatStr = ">" + formatStr;
      return string.Format(formatStr, text);
    }
  }
}
