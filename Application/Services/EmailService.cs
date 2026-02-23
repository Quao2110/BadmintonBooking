using Application.Interfaces.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var emailSettings = _configuration.GetSection("EmailConfig");
        
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(emailSettings["Email"]));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = message };
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["Port"]!), SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(emailSettings["Email"], emailSettings["Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

    public async Task SendOtpEmailAsync(string toEmail, string otp)
    {
        string subject = "🔐 Mã xác thực OTP của bạn - Badminton Booking";

        string message = $"""
            <!DOCTYPE html>
            <html lang="vi">
            <head>
                <meta charset="UTF-8" />
                <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
                <title>Mã OTP xác thực</title>
            </head>
            <body style="margin:0;padding:0;background-color:#f4f6fb;font-family:'Segoe UI',Arial,sans-serif;">
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color:#f4f6fb;padding:40px 0;">
                    <tr>
                        <td align="center">
                            <table width="600" cellpadding="0" cellspacing="0" style="background:#ffffff;border-radius:16px;overflow:hidden;box-shadow:0 4px 24px rgba(0,0,0,0.08);">

                                <!-- HEADER -->
                                <tr>
                                    <td style="background:linear-gradient(135deg,#1a73e8 0%,#0d47a1 100%);padding:40px 48px;text-align:center;">
                                        <div style="font-size:28px;font-weight:800;color:#ffffff;letter-spacing:1px;">🏸 Badminton Booking</div>
                                        <div style="font-size:13px;color:#a8c7fa;margin-top:6px;letter-spacing:2px;text-transform:uppercase;">Xác thực tài khoản</div>
                                    </td>
                                </tr>

                                <!-- BODY -->
                                <tr>
                                    <td style="padding:48px 48px 32px;">
                                        <p style="margin:0 0 12px;font-size:16px;color:#3c4043;">Xin chào,</p>
                                        <p style="margin:0 0 32px;font-size:15px;color:#5f6368;line-height:1.6;">
                                            Chúng tôi đã nhận được yêu cầu xác thực từ bạn. Vui lòng sử dụng mã OTP dưới đây để hoàn tất thao tác.
                                        </p>

                                        <!-- OTP BOX -->
                                        <div style="background:linear-gradient(135deg,#e8f0fe 0%,#d2e3fc 100%);border:2px dashed #1a73e8;border-radius:12px;padding:32px;text-align:center;margin-bottom:32px;">
                                            <div style="font-size:13px;color:#1a73e8;font-weight:600;letter-spacing:2px;text-transform:uppercase;margin-bottom:12px;">Mã xác thực OTP</div>
                                            <div style="font-size:52px;font-weight:900;letter-spacing:16px;color:#0d47a1;font-family:'Courier New',monospace;">{otp}</div>
                                            <div style="margin-top:14px;font-size:13px;color:#5f6368;">⏱ Mã có hiệu lực trong <strong style="color:#d93025;">5 phút</strong></div>
                                        </div>

                                        <!-- WARNING -->
                                        <div style="background:#fff8e1;border-left:4px solid #f9a825;border-radius:0 8px 8px 0;padding:16px 20px;margin-bottom:24px;">
                                            <p style="margin:0;font-size:13px;color:#795548;line-height:1.6;">
                                                ⚠️ <strong>Lưu ý bảo mật:</strong> Không chia sẻ mã này với bất kỳ ai. Đội ngũ Badminton Booking <strong>sẽ không bao giờ</strong> yêu cầu mã OTP của bạn.
                                            </p>
                                        </div>

                                        <p style="margin:0;font-size:14px;color:#5f6368;line-height:1.6;">
                                            Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này. Tài khoản của bạn vẫn an toàn.
                                        </p>
                                    </td>
                                </tr>

                                <!-- DIVIDER -->
                                <tr>
                                    <td style="padding:0 48px;">
                                        <hr style="border:none;border-top:1px solid #e8eaed;margin:0;" />
                                    </td>
                                </tr>

                                <!-- FOOTER -->
                                <tr>
                                    <td style="padding:24px 48px 40px;text-align:center;">
                                        <p style="margin:0 0 4px;font-size:12px;color:#9aa0a6;">Email này được gửi tự động, vui lòng không trả lời.</p>
                                        <p style="margin:0;font-size:12px;color:#9aa0a6;">© 2026 <strong>Badminton Booking</strong>. All rights reserved.</p>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>
            """;

        await SendEmailAsync(toEmail, subject, message);
    }
}
