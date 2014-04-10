using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace AkaneMail
{
  public abstract class MailBoxWriter
  {
    /// <summary>
    /// 書き込まれる Stream を取得します。
    /// </summary>
    public Stream Stream { get; private set; }
    /// <summary>
    /// 書き込む MailBoxを取得します。
    /// </summary>
    public MailBox MailBox { get; private set; }


    /// <summary>
    /// 指定された MailBox と Stream から MailBoxWriter クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="mailBox">書き込む MailBox</param>
    /// <param name="stream">書き込まれる Stream</param>
    public MailBoxWriter(MailBox mailBox, Stream stream)
    {
      if (mailBox == null)
        throw new ArgumentNullException("mailBox が null です。");
      if (stream == null)
        throw new ArgumentNullException("stream が null です。");
      if (!stream.CanWrite)
        throw new ArgumentException("書き込みが許可されていない stream です");
      Stream = stream;
      MailBox = mailBox;
    }

    /// <summary>
    /// 指定された MailBox と filePath から mailBoxWriter クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="mailBox">書き込む MailBox</param>
    /// <param name="filePath">書き込まれるファイルのパス</param>
    public MailBoxWriter(MailBox mailBox, string filePath)
      : this(mailBox, new FileStream(filePath, FileMode.Create))
    { }

    /// <summary>
    /// MailBox を mbox 形式で書き込みます。
    /// </summary>
    public abstract void Write();
  }
}
