using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Configurations;
internal class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.RegisterationTime)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.Country)
            .HasMaxLength(100);

        builder.Property(c => c.Phone)
            .HasMaxLength(20);

        builder.HasMany(c => c.Notifications)
                  .WithOne(n => n.Client)
                  .HasForeignKey(n => n.ClientId);

        #region Seeding Data
        builder.HasData(
                 new Client
                 {
                     Id = 1,
                     Name = "عبد الرحمن أحمد",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مبرمج ومطور تطبيقات متخصص في تطوير الويب",
                     Country = "المملكة العربية السعودية",
                     Phone = "+966123456789"
                 },
                 new Client
                 {
                     Id = 2,
                     Name = "فاطمة محمد",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مصممة جرافيك محترفة تعمل في تصميم اللوجوهات والبوسترات",
                     Country = "مصر",
                     Phone = "+201234567890"
                 },
                 new Client
                 {
                     Id = 3,
                     Name = "علي العبدالله",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مسوق رقمي محترف بخبرة في إدارة الحملات الإعلانية عبر وسائل التواصل الاجتماعي",
                     Country = "الإمارات العربية المتحدة",
                     Phone = "+971123456789"
                 },
                 new Client
                 {
                     Id = 4,
                     Name = "مريم حسن",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "كاتبة محتوى متخصصة في الكتابة الإبداعية والمقالات الفنية",
                     Country = "الأردن",
                     Phone = "+962123456789"
                 },
                 new Client
                 {
                     Id = 5,
                     Name = "يوسف خالد",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مصور فوتوغرافي متخصص في تصوير الأحداث والمناسبات الخاصة",
                     Country = "العراق",
                     Phone = "+964123456789"
                 },
                 new Client
                 {
                     Id = 6,
                     Name = "لمى عبدالله",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مديرة مشروع محترفة في إدارة المشاريع التقنية والتطوير البرمجي",
                     Country = "السعودية",
                     Phone = "+966123456789"
                 },
                 new Client
                 {
                     Id = 7,
                     Name = "عمر أحمد",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مسوق محتوى إبداعي يعمل على ترويج المحتوى الرقمي للشركات الناشئة",
                     Country = "مصر",
                     Phone = "+201234567890"
                 },
                 new Client
                 {
                     Id = 8,
                     Name = "رنا محمود",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مطورة تطبيقات محترفة تعمل في تطوير تطبيقات الهواتف الذكية",
                     Country = "لبنان",
                     Phone = "+961123456789"
                 },
                 new Client
                 {
                     Id = 9,
                     Name = "أحمد علي",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مدير تسويق متخصص في إدارة استراتيجيات التسويق الرقمي",
                     Country = "الأردن",
                     Phone = "+962123456789"
                 },
                 new Client
                 {
                     Id = 10,
                     Name = "هدى صالح",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "خبيرة في تصميم وإدارة مواقع الويب للشركات الصغيرة والمتوسطة",
                     Country = "السعودية",
                     Phone = "+966123456789"
                 },
                  new Client
                  {
                      Id = 11,
                      Name = "سلمى عبدالله",
                      RegisterationTime = new DateTime(2025, 6, 12),
                      Description = "محاسبة مالية محترفة تعمل في مجال إعداد التقارير المالية",
                      Country = "الإمارات العربية المتحدة",
                      Phone = "+971123456789"
                  },
                 new Client
                 {
                     Id = 12,
                     Name = "محمد حسن",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مهندس معماري متخصص في تصميم المباني السكنية",
                     Country = "مصر",
                     Phone = "+201234567890"
                 },
                 new Client
                 {
                     Id = 13,
                     Name = "زينب عبدالله",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "طبيبة مختصة في طب الأطفال والأمراض النفسية",
                     Country = "العراق",
                     Phone = "+964123456789"
                 },
                 new Client
                 {
                     Id = 14,
                     Name = "أحمد حسين",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مصمم جرافيك مبدع يعمل في تصميم الإعلانات التجارية",
                     Country = "لبنان",
                     Phone = "+961123456789"
                 },
                 new Client
                 {
                     Id = 15,
                     Name = "فاطمة علي",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مترجمة محترفة تعمل في ترجمة النصوص الطبية والعلمية",
                     Country = "الأردن",
                     Phone = "+962123456789"
                 },
                 new Client
                 {
                     Id = 16,
                     Name = "عبدالله محمود",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مطور ويب محترف في تطوير التطبيقات الإلكترونية",
                     Country = "السعودية",
                     Phone = "+966123456789"
                 },
                 new Client
                 {
                     Id = 17,
                     Name = "ريم عبدالله",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "مهندسة معمارية متخصصة في تصميم المنشآت الصناعية",
                     Country = "مصر",
                     Phone = "+201234567890"
                 },
                 new Client
                 {
                     Id = 18,
                     Name = "عمر حسن",
                     RegisterationTime = new DateTime(2025, 6, 12),
                     Description = "محاسب مالي يتمتع بخبرة واسعة في المحاسبة المالية",
                     Country = "لبنان",
                     Phone = "+961123456789"
                 }
                 );
        #endregion

    }
}
