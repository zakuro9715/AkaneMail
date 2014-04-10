using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AkaneMail
{
  public class MboxWriter : MailBoxWriter
  {    /// <summary>
    /// 指定された MailBox と Stream から MboxWriter クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="mailBox">書き込む MailBox</param>
    /// <param name="stream">書き込まれる Stream</param>
    public MboxWriter(MailBox mailBox, Stream stream) : base(mailBox, stream) { }
    /// <summary>
    /// 指定された MailBox と filePath から MboxWriter クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="mailBox">書き込む MailBox</param>
    /// <param name="filePath">書き込まれるファイルのパス</param>
    public MboxWriter(MailBox mailBox, string filePath) : base(mailBox, filePath) { }

    /// <summary>
    /// MailBox を mbox 形式で書き込みます。
    /// </summary>
    public override void Write()
    {
      using (var writer = new StreamWriter(Stream))
      {
        foreach (var mail in MailBox.Receive)
          writer.WriteLine(FormatedMail(mail));
      }
    }

    private string FormatedMail(Mail mail)
    {
      var sb = new StringBuilder();
      sb.Append(BuildFromLine(mail));
      using (var reader = new StringReader(BuildFromLine(mail) + mail.header + mail.body))
      {
        string s;
        while ((s = reader.ReadLine()) != null)
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
