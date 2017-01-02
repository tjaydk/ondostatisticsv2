using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class OndoDB_2013_3_4_8_15Context : DbContext
    {
        public virtual DbSet<TblAdmin> TblAdmin { get; set; }
        public virtual DbSet<TblAppId> TblAppId { get; set; }
        public virtual DbSet<TblAutoPost> TblAutoPost { get; set; }
        public virtual DbSet<TblBenefit> TblBenefit { get; set; }
        public virtual DbSet<TblBenefitImages> TblBenefitImages { get; set; }
        public virtual DbSet<TblBenefitProgramRelation> TblBenefitProgramRelation { get; set; }
        public virtual DbSet<TblBenefitPrograms> TblBenefitPrograms { get; set; }
        public virtual DbSet<TblBenefitProposals> TblBenefitProposals { get; set; }
        public virtual DbSet<TblBenefitRelation> TblBenefitRelation { get; set; }
        public virtual DbSet<TblBlockedIp> TblBlockedIp { get; set; }
        public virtual DbSet<TblCategoryNames> TblCategoryNames { get; set; }
        public virtual DbSet<TblClubActivity> TblClubActivity { get; set; }
        public virtual DbSet<TblCommentNotifications> TblCommentNotifications { get; set; }
        public virtual DbSet<TblComments> TblComments { get; set; }
        public virtual DbSet<TblConfigurationMembers> TblConfigurationMembers { get; set; }
        public virtual DbSet<TblCustomImages> TblCustomImages { get; set; }
        public virtual DbSet<TblDiceStatistics> TblDiceStatistics { get; set; }
        public virtual DbSet<TblDropsterConfiguration> TblDropsterConfiguration { get; set; }
        public virtual DbSet<TblDropsterDevices> TblDropsterDevices { get; set; }
        public virtual DbSet<TblDropsterLog> TblDropsterLog { get; set; }
        public virtual DbSet<TblDropsterPayment> TblDropsterPayment { get; set; }
        public virtual DbSet<TblDropsterRecord> TblDropsterRecord { get; set; }
        public virtual DbSet<TblDropsterSms> TblDropsterSms { get; set; }
        public virtual DbSet<TblDropsterTickets> TblDropsterTickets { get; set; }
        public virtual DbSet<TblDropsterUser> TblDropsterUser { get; set; }
        public virtual DbSet<TblEconomics> TblEconomics { get; set; }
        public virtual DbSet<TblEconomicsRelation> TblEconomicsRelation { get; set; }
        public virtual DbSet<TblEncryptedCards> TblEncryptedCards { get; set; }
        public virtual DbSet<TblEncryptedCardsAdmin> TblEncryptedCardsAdmin { get; set; }
        public virtual DbSet<TblExternalAdmin> TblExternalAdmin { get; set; }
        public virtual DbSet<TblFacebookStatistics> TblFacebookStatistics { get; set; }
        public virtual DbSet<TblFbbenefit> TblFbbenefit { get; set; }
        public virtual DbSet<TblFbpost> TblFbpost { get; set; }
        public virtual DbSet<TblGameStatistics> TblGameStatistics { get; set; }
        public virtual DbSet<TblGhostImages> TblGhostImages { get; set; }
        public virtual DbSet<TblHashTags> TblHashTags { get; set; }
        public virtual DbSet<TblHubNotifications> TblHubNotifications { get; set; }
        public virtual DbSet<TblInformationLevel> TblInformationLevel { get; set; }
        public virtual DbSet<TblLogging> TblLogging { get; set; }
        public virtual DbSet<TblLotteryDrawings> TblLotteryDrawings { get; set; }
        public virtual DbSet<TblLotteryTickets> TblLotteryTickets { get; set; }
        public virtual DbSet<TblMerchantIds> TblMerchantIds { get; set; }
        public virtual DbSet<TblNetsGiftCard> TblNetsGiftCard { get; set; }
        public virtual DbSet<TblNotifications> TblNotifications { get; set; }
        public virtual DbSet<TblOndoAdditionals> TblOndoAdditionals { get; set; }
        public virtual DbSet<TblOndoAdminRelation> TblOndoAdminRelation { get; set; }
        public virtual DbSet<TblOndoLottery> TblOndoLottery { get; set; }
        public virtual DbSet<TblOndoLotteryRelation> TblOndoLotteryRelation { get; set; }
        public virtual DbSet<TblOndoRelation> TblOndoRelation { get; set; }
        public virtual DbSet<TblOndoRestaurantRelation> TblOndoRestaurantRelation { get; set; }
        public virtual DbSet<TblOndos> TblOndos { get; set; }
        public virtual DbSet<TblPasswordRequest> TblPasswordRequest { get; set; }
        public virtual DbSet<TblPayment> TblPayment { get; set; }
        public virtual DbSet<TblPaymentTransaction> TblPaymentTransaction { get; set; }
        public virtual DbSet<TblPlannedPosts> TblPlannedPosts { get; set; }
        public virtual DbSet<TblPostNotifications> TblPostNotifications { get; set; }
        public virtual DbSet<TblPushChannels> TblPushChannels { get; set; }
        public virtual DbSet<TblPushNotificationStats> TblPushNotificationStats { get; set; }
        public virtual DbSet<TblReservedTransactions> TblReservedTransactions { get; set; }
        public virtual DbSet<TblRssPosts> TblRssPosts { get; set; }
        public virtual DbSet<TblSettlements> TblSettlements { get; set; }
        public virtual DbSet<TblSettlementsMonth> TblSettlementsMonth { get; set; }
        public virtual DbSet<TblSms> TblSms { get; set; }
        public virtual DbSet<TblSocialMedia> TblSocialMedia { get; set; }
        public virtual DbSet<TblStatusInformation> TblStatusInformation { get; set; }
        public virtual DbSet<TblTrace> TblTrace { get; set; }
        public virtual DbSet<TblUserBalance> TblUserBalance { get; set; }
        public virtual DbSet<TblUserBalanceCard> TblUserBalanceCard { get; set; }
        public virtual DbSet<TblUserEvent> TblUserEvent { get; set; }
        public virtual DbSet<TblUserGameStatus> TblUserGameStatus { get; set; }
        public virtual DbSet<TblUserLocation> TblUserLocation { get; set; }
        public virtual DbSet<TblUserSetlementTransactions> TblUserSetlementTransactions { get; set; }
        public virtual DbSet<TblUserSettlementBalance> TblUserSettlementBalance { get; set; }
        public virtual DbSet<TblUserStatistics> TblUserStatistics { get; set; }
        public virtual DbSet<TblUserStatus> TblUserStatus { get; set; }
        public virtual DbSet<TblUserTransactions> TblUserTransactions { get; set; }
        public virtual DbSet<TblUserVerification> TblUserVerification { get; set; }
        public virtual DbSet<TblUserVouchers> TblUserVouchers { get; set; }
        public virtual DbSet<TblUsers> TblUsers { get; set; }
        public virtual DbSet<TblUsersToWelcome> TblUsersToWelcome { get; set; }
        public virtual DbSet<TblVirtualUsers> TblVirtualUsers { get; set; }
        public virtual DbSet<TblVoucherPrograms> TblVoucherPrograms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["OndoDb"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAdmin>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PrimaryKey_64067712-e3d2-4b57-9703-5a9fd34ec219");

                entity.ToTable("tblAdmin");

                entity.Property(e => e.GenerateTime).HasMaxLength(50);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblAppId>(entity =>
            {
                entity.ToTable("tblAppId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AboutLink).HasMaxLength(150);

                entity.Property(e => e.ActionByte).HasDefaultValueSql("0");

                entity.Property(e => e.AndroidStoreLink).HasMaxLength(150);

                entity.Property(e => e.AppName).HasMaxLength(50);

                entity.Property(e => e.BarColor).HasMaxLength(10);

                entity.Property(e => e.HeaderColor).HasMaxLength(10);

                entity.Property(e => e.HelpLink).HasMaxLength(150);

                entity.Property(e => e.IosstoreLink)
                    .HasColumnName("IOSStoreLink")
                    .HasMaxLength(150);

                entity.Property(e => e.MainOndo).HasDefaultValueSql("0");

                entity.Property(e => e.MapIconContact).HasMaxLength(150);

                entity.Property(e => e.MapIconMember).HasMaxLength(150);

                entity.Property(e => e.MapIconOwner).HasMaxLength(150);

                entity.Property(e => e.MapIconViewer).HasMaxLength(150);

                entity.Property(e => e.PrivacyLink).HasMaxLength(150);

                entity.Property(e => e.ServerType).HasDefaultValueSql("0");

                entity.Property(e => e.StartUpView).HasDefaultValueSql("0");

                entity.Property(e => e.SupportMail).HasMaxLength(50);

                entity.Property(e => e.TermsLink).HasMaxLength(150);

                entity.Property(e => e.WindowsStoreLink).HasMaxLength(150);
            });

            modelBuilder.Entity<TblAutoPost>(entity =>
            {
                entity.ToTable("tblAutoPost");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblBenefit>(entity =>
            {
                entity.HasKey(e => e.BenefitId)
                    .HasName("PrimaryKey_ca03b36c-e638-4cf9-9830-3811f19e7b0f");

                entity.ToTable("tblBenefit");

                entity.Property(e => e.AccountId).HasMaxLength(50);

                entity.Property(e => e.ActionByte).HasDefaultValueSql("0");

                entity.Property(e => e.Bonus).HasDefaultValueSql("-1");

                entity.Property(e => e.CategoryByte).HasDefaultValueSql("0");

                entity.Property(e => e.Chances).HasDefaultValueSql("0");

                entity.Property(e => e.Conditions).HasMaxLength(320);

                entity.Property(e => e.Curfew).HasDefaultValueSql("0");

                entity.Property(e => e.Distance).HasDefaultValueSql("0");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Items).HasDefaultValueSql("0");

                entity.Property(e => e.MiniPicture).HasMaxLength(150);

                entity.Property(e => e.NewsLetterPicture).HasMaxLength(200);

                entity.Property(e => e.NormalPrice).HasDefaultValueSql("-1");

                entity.Property(e => e.OfferPrice).HasDefaultValueSql("-1");

                entity.Property(e => e.OndoRef).HasDefaultValueSql("-1");

                entity.Property(e => e.OnlyImage).HasDefaultValueSql("0");

                entity.Property(e => e.OriginalPicture).HasMaxLength(200);

                entity.Property(e => e.Picture).HasMaxLength(150);

                entity.Property(e => e.PriceId).HasDefaultValueSql("0");

                entity.Property(e => e.SocialMediaString).HasMaxLength(280);

                entity.Property(e => e.Stamp).HasMaxLength(150);

                entity.Property(e => e.StampBackground).HasMaxLength(10);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Text).HasMaxLength(320);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.WinnerText).HasMaxLength(320);
            });

            modelBuilder.Entity<TblBenefitImages>(entity =>
            {
                entity.ToTable("tblBenefitImages");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BenefitType).HasDefaultValueSql("0");

                entity.Property(e => e.Count).HasDefaultValueSql("0");

                entity.Property(e => e.Height).HasDefaultValueSql("0");

                entity.Property(e => e.Picture).HasMaxLength(150);

                entity.Property(e => e.Type).HasDefaultValueSql("0");

                entity.Property(e => e.Width).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblBenefitProgramRelation>(entity =>
            {
                entity.ToTable("tblBenefitProgramRelation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionByte).HasDefaultValueSql("-1");

                entity.Property(e => e.PercentString).HasMaxLength(150);

                entity.Property(e => e.PermitsByte).HasDefaultValueSql("1");

                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblBenefitPrograms>(entity =>
            {
                entity.ToTable("tblBenefitPrograms");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionByte).HasDefaultValueSql("1");

                entity.Property(e => e.BarcodeLength).HasDefaultValueSql("19");

                entity.Property(e => e.BenefitId).HasDefaultValueSql("0");

                entity.Property(e => e.BenefitType).HasDefaultValueSql("0");

                entity.Property(e => e.CanDelete).HasDefaultValueSql("0");

                entity.Property(e => e.CardLengthByte).HasMaxLength(20);

                entity.Property(e => e.CardPrice)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("-1");

                entity.Property(e => e.CodeRangeFrom).HasMaxLength(20);

                entity.Property(e => e.CodeRangeTo).HasMaxLength(20);

                entity.Property(e => e.Currency).HasMaxLength(50);

                entity.Property(e => e.CurrentEcard).HasMaxLength(20);

                entity.Property(e => e.EcardPrice)
                    .IsRequired()
                    .HasColumnName("ECardPrice")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("-1");

                entity.Property(e => e.Link).HasMaxLength(250);

                entity.Property(e => e.MaxTransactions).HasDefaultValueSql("-1");

                entity.Property(e => e.MinTransactions).HasDefaultValueSql("-1");

                entity.Property(e => e.PercentString).HasMaxLength(150);

                entity.Property(e => e.PointValue)
                    .HasColumnType("decimal")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SenderId).HasMaxLength(50);

                entity.Property(e => e.SheetValue)
                    .HasColumnType("decimal")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TransactionMaxLevel).HasDefaultValueSql("0");

                entity.Property(e => e.TransactionPrice)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("-1");

                entity.Property(e => e.TransactionWarningLevel).HasDefaultValueSql("0");

                entity.Property(e => e.Type).HasDefaultValueSql("0");

                entity.Property(e => e.UserToWarn).HasDefaultValueSql("0");

                entity.Property(e => e.ValidDays).HasDefaultValueSql("0");

                entity.Property(e => e.WalletUri).HasMaxLength(150);
            });

            modelBuilder.Entity<TblBenefitProposals>(entity =>
            {
                entity.HasKey(e => e.BenefitProposalId)
                    .HasName("PK__Table__58F382D912EA3BE6");

                entity.ToTable("tblBenefitProposals");

                entity.Property(e => e.Bonus).HasDefaultValueSql("-1");

                entity.Property(e => e.Conditions).HasMaxLength(300);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Height).HasDefaultValueSql("0");

                entity.Property(e => e.Label).HasDefaultValueSql("0");

                entity.Property(e => e.MiniPicture).HasMaxLength(250);

                entity.Property(e => e.NormalPrice).HasDefaultValueSql("-1");

                entity.Property(e => e.OfferPrice).HasDefaultValueSql("-1");

                entity.Property(e => e.OndoId).HasDefaultValueSql("0");

                entity.Property(e => e.OriginalPicture).HasMaxLength(250);

                entity.Property(e => e.Picture).HasMaxLength(250);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.Text).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Type).HasDefaultValueSql("0");

                entity.Property(e => e.Width).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblBenefitRelation>(entity =>
            {
                entity.HasKey(e => e.RelationId)
                    .HasName("PK_tblBenefitRelation");

                entity.ToTable("tblBenefitRelation");
            });

            modelBuilder.Entity<TblBlockedIp>(entity =>
            {
                entity.ToTable("tblBlockedIP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.IpAddress).HasMaxLength(50);

                entity.Property(e => e.Text).HasMaxLength(250);
            });

            modelBuilder.Entity<TblCategoryNames>(entity =>
            {
                entity.ToTable("tblCategoryNames");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Value).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblClubActivity>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("PrimaryKey_a7275b4a-1034-47f0-83b6-f532698eefde");

                entity.ToTable("tblClubActivity");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<TblCommentNotifications>(entity =>
            {
                entity.ToTable("tblCommentNotifications");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasDefaultValueSql("0");

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblComments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PrimaryKey_f03d2275-8f3b-40a5-8f89-b22cd4e2a238");

                entity.ToTable("tblComments");

                entity.Property(e => e.Content).HasMaxLength(200);

                entity.Property(e => e.ContentType).HasMaxLength(50);

                entity.Property(e => e.GeoTag).HasMaxLength(150);

                entity.Property(e => e.MiniPicture).HasMaxLength(150);

                entity.Property(e => e.NotStatus).HasMaxLength(150);

                entity.Property(e => e.Text).HasMaxLength(320);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<TblConfigurationMembers>(entity =>
            {
                entity.ToTable("tblConfigurationMembers");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TblCustomImages>(entity =>
            {
                entity.ToTable("tblCustomImages");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Uri).HasMaxLength(150);
            });

            modelBuilder.Entity<TblDiceStatistics>(entity =>
            {
                entity.ToTable("tblDiceStatistics");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TblDropsterConfiguration>(entity =>
            {
                entity.ToTable("tblDropsterConfiguration");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BagPrice).HasColumnType("decimal");

                entity.Property(e => e.Culture).HasMaxLength(50);

                entity.Property(e => e.Jacketprice).HasColumnType("decimal");

                entity.Property(e => e.MerchantId).HasMaxLength(50);
            });

            modelBuilder.Entity<TblDropsterDevices>(entity =>
            {
                entity.ToTable("tblDropsterDevices");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DeviceId).HasMaxLength(300);
            });

            modelBuilder.Entity<TblDropsterLog>(entity =>
            {
                entity.ToTable("tblDropsterLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Text).HasMaxLength(140);
            });

            modelBuilder.Entity<TblDropsterPayment>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PrimaryKey_760f208c-7846-4e70-8f04-7ea967bcc484");

                entity.ToTable("tblDropsterPayment");

                entity.Property(e => e.Amount).HasColumnType("decimal");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.ErrorCode).HasDefaultValueSql("-1");

                entity.Property(e => e.IsSynchronized).HasDefaultValueSql("0");

                entity.Property(e => e.MerchantId).HasMaxLength(50);

                entity.Property(e => e.PayLink).HasMaxLength(250);

                entity.Property(e => e.ReceiptMessage).HasMaxLength(80);

                entity.Property(e => e.Tickets).HasMaxLength(100);

                entity.Property(e => e.TransactionId).HasMaxLength(50);

                entity.Property(e => e.Unit).HasMaxLength(50);
            });

            modelBuilder.Entity<TblDropsterRecord>(entity =>
            {
                entity.ToTable("tblDropsterRecord");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TblDropsterSms>(entity =>
            {
                entity.ToTable("tblDropsterSMS");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TblDropsterTickets>(entity =>
            {
                entity.ToTable("tblDropsterTickets");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Uri).HasMaxLength(200);

                entity.Property(e => e.UserIdRequest).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblDropsterUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PrimaryKey_b0975a51-dcf3-4817-9b31-7050e0e5eec3");

                entity.ToTable("tblDropsterUser");

                entity.Property(e => e.Code).HasMaxLength(5);

                entity.Property(e => e.Culture).HasMaxLength(7);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.PhoneBarCode).HasMaxLength(150);

                entity.Property(e => e.PhonePrefix).HasMaxLength(5);

                entity.Property(e => e.Picture).HasMaxLength(200);

                entity.Property(e => e.PushChannel).HasMaxLength(300);

                entity.Property(e => e.Region).HasMaxLength(5);

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblEconomics>(entity =>
            {
                entity.ToTable("tblEconomics");

                entity.Property(e => e.CardCountTotal).HasDefaultValueSql("0");

                entity.Property(e => e.Cvr)
                    .HasColumnName("CVR")
                    .HasMaxLength(50);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.EarnedPoints).HasMaxLength(50);

                entity.Property(e => e.EcardCountTotal)
                    .HasColumnName("ECardCountTotal")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Fee).HasMaxLength(50);

                entity.Property(e => e.FeeMin).HasMaxLength(5);

                entity.Property(e => e.FeePercent).HasMaxLength(5);

                entity.Property(e => e.InvoicedTransactions).HasDefaultValueSql("-1");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OndoId).HasDefaultValueSql("-1");

                entity.Property(e => e.Payments).HasDefaultValueSql("-1");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.SumFrom).HasMaxLength(300);

                entity.Property(e => e.Text).HasMaxLength(150);

                entity.Property(e => e.TotalAmount).HasMaxLength(50);

                entity.Property(e => e.TotalAmountCharge).HasMaxLength(50);

                entity.Property(e => e.TotalEarnedPoints).HasDefaultValueSql("0");

                entity.Property(e => e.TotalOnlineTransactions).HasDefaultValueSql("0");

                entity.Property(e => e.Transactions).HasDefaultValueSql("-1");

                entity.Property(e => e.TransferAmount).HasMaxLength(50);

                entity.Property(e => e.Week).HasDefaultValueSql("-1");

                entity.Property(e => e.Year).HasDefaultValueSql("-1");
            });

            modelBuilder.Entity<TblEconomicsRelation>(entity =>
            {
                entity.ToTable("tblEconomicsRelation");

                entity.Property(e => e.AmountCharge).HasMaxLength(50);

                entity.Property(e => e.BenefitId).HasDefaultValueSql("0");

                entity.Property(e => e.CardCount).HasDefaultValueSql("0");

                entity.Property(e => e.CardPrice)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CountAmountCharge).HasDefaultValueSql("-1");

                entity.Property(e => e.EcardCount)
                    .HasColumnName("ECardCount")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.EcardPrize)
                    .IsRequired()
                    .HasColumnName("ECardPrize")
                    .HasMaxLength(5)
                    .HasDefaultValueSql("0");

                entity.Property(e => e.EconomicsId).HasDefaultValueSql("0");

                entity.Property(e => e.GamePrice).HasMaxLength(50);

                entity.Property(e => e.InvoicedTransactions).HasDefaultValueSql("-1");

                entity.Property(e => e.MaxTransactions).HasDefaultValueSql("0");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OndoId).HasDefaultValueSql("0");

                entity.Property(e => e.OnlineTransactions).HasDefaultValueSql("0");

                entity.Property(e => e.PointValue)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Points).HasDefaultValueSql("0");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.TransactionPrice)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Transactions).HasDefaultValueSql("0");

                entity.Property(e => e.TransferAmount)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblEncryptedCards>(entity =>
            {
                entity.ToTable("tblEncryptedCards");

                entity.Property(e => e.BenefitId).HasDefaultValueSql("0");

                entity.Property(e => e.CardNr).HasMaxLength(16);

                entity.Property(e => e.CityCode).HasMaxLength(5);

                entity.Property(e => e.Picture).HasMaxLength(200);

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.UnCrypt).HasMaxLength(16);
            });

            modelBuilder.Entity<TblEncryptedCardsAdmin>(entity =>
            {
                entity.ToTable("tblEncryptedCardsAdmin");

                entity.Property(e => e.BenefitId).HasDefaultValueSql("-1");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.CityCode).HasMaxLength(5);

                entity.Property(e => e.RangeFrom).HasDefaultValueSql("0");

                entity.Property(e => e.RangeTo).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblExternalAdmin>(entity =>
            {
                entity.ToTable("tblExternalAdmin");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.OndoId).HasDefaultValueSql("-1");

                entity.Property(e => e.Status).HasDefaultValueSql("-1");

                entity.Property(e => e.Token).HasMaxLength(300);

                entity.Property(e => e.UserId).HasDefaultValueSql("-1");
            });

            modelBuilder.Entity<TblFacebookStatistics>(entity =>
            {
                entity.ToTable("tblFacebookStatistics");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblFbbenefit>(entity =>
            {
                entity.ToTable("tblFBBenefit");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fbpost)
                    .HasColumnName("FBPost")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblFbpost>(entity =>
            {
                entity.ToTable("tblFBPost");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fbpost)
                    .HasColumnName("FBPost")
                    .HasMaxLength(50);

                entity.Property(e => e.PlannedPostId).HasDefaultValueSql("0");

                entity.Property(e => e.PostId).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblGameStatistics>(entity =>
            {
                entity.HasKey(e => e.StatisticsId)
                    .HasName("PrimaryKey_c21edb94-97c8-4c27-bed3-ff503401e886");

                entity.ToTable("tblGameStatistics");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblGhostImages>(entity =>
            {
                entity.ToTable("tblGhostImages");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Uri).HasMaxLength(250);
            });

            modelBuilder.Entity<TblHashTags>(entity =>
            {
                entity.ToTable("tblHashTags");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Text).HasMaxLength(50);
            });

            modelBuilder.Entity<TblHubNotifications>(entity =>
            {
                entity.ToTable("tblHubNotifications");

                entity.Property(e => e.Comments).HasMaxLength(300);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Posts).HasMaxLength(300);
            });

            modelBuilder.Entity<TblInformationLevel>(entity =>
            {
                entity.ToTable("tblInformationLevel");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionByte).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblLogging>(entity =>
            {
                entity.ToTable("tblLogging");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.IpAddress).HasMaxLength(20);

                entity.Property(e => e.Text).HasMaxLength(100);
            });

            modelBuilder.Entity<TblLotteryDrawings>(entity =>
            {
                entity.HasKey(e => e.DrawingId)
                    .HasName("PrimaryKey_411abf9e-8a02-432e-a5b3-c926b43337d6");

                entity.ToTable("tblLotteryDrawings");

                entity.Property(e => e.DrawingTime).HasColumnType("datetime");

                entity.Property(e => e.DrawingWinnerText).HasMaxLength(300);

                entity.Property(e => e.InformWinner).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblLotteryTickets>(entity =>
            {
                entity.HasKey(e => e.TicketId)
                    .HasName("PrimaryKey_972ee811-a271-496a-abdd-f9213176f49f");

                entity.ToTable("tblLotteryTickets");
            });

            modelBuilder.Entity<TblMerchantIds>(entity =>
            {
                entity.ToTable("tblMerchantIds");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MinimumAmount).HasColumnType("decimal");

                entity.Property(e => e.MobilePay).HasMaxLength(50);
            });

            modelBuilder.Entity<TblNetsGiftCard>(entity =>
            {
                entity.HasKey(e => e.NetsGiftCardId)
                    .HasName("PrimaryKey_6c3ac719-e9bc-4100-9307-397e90cf9a67");

                entity.ToTable("tblNetsGiftCard");

                entity.Property(e => e.BarCodePicture).HasMaxLength(150);

                entity.Property(e => e.Cardnr).HasMaxLength(150);
            });

            modelBuilder.Entity<TblNotifications>(entity =>
            {
                entity.HasKey(e => e.NotificationId)
                    .HasName("PrimaryKey_6bc4f9ec-55d1-4c74-b8ea-d3f94b401b41");

                entity.ToTable("tblNotifications");

                entity.Property(e => e.Text).HasMaxLength(150);

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblOndoAdditionals>(entity =>
            {
                entity.ToTable("tblOndoAdditionals");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.BadgPosition).HasDefaultValueSql("0");

                entity.Property(e => e.Badge).HasMaxLength(150);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.ContactInfo).HasMaxLength(150);

                entity.Property(e => e.ContentWithBadge).HasMaxLength(150);

                entity.Property(e => e.Cvr)
                    .HasColumnName("CVR")
                    .HasMaxLength(50);

                entity.Property(e => e.DefaultWelcomeComment).HasMaxLength(320);

                entity.Property(e => e.District).HasMaxLength(50);

                entity.Property(e => e.ExcludeJoinsInFeed).HasDefaultValueSql("0");

                entity.Property(e => e.InfoByte).HasDefaultValueSql("0");

                entity.Property(e => e.LocateFromOndo).HasDefaultValueSql("0");

                entity.Property(e => e.MaxParticipants).HasDefaultValueSql("500");

                entity.Property(e => e.OndoId).HasDefaultValueSql("0");

                entity.Property(e => e.OpeningHoursHeader).HasMaxLength(50);

                entity.Property(e => e.OpeningHoursText).HasMaxLength(320);

                entity.Property(e => e.PdfMiniPicture).HasMaxLength(150);

                entity.Property(e => e.PdfPicture).HasMaxLength(150);

                entity.Property(e => e.PdfUrl).HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.PostalCode).HasMaxLength(50);

                entity.Property(e => e.QrUri).HasMaxLength(150);

                entity.Property(e => e.RemoteSignup).HasDefaultValueSql("0");

                entity.Property(e => e.Rssfeed)
                    .HasColumnName("RSSFeed")
                    .HasMaxLength(150);

                entity.Property(e => e.Rssinterval)
                    .HasColumnName("RSSInterval")
                    .HasDefaultValueSql("60");

                entity.Property(e => e.RsstimeStamp)
                    .HasColumnName("RSSTimeStamp")
                    .HasColumnType("datetime");

                entity.Property(e => e.RssupdateTime)
                    .HasColumnName("RSSUpdateTime")
                    .HasMaxLength(50);

                entity.Property(e => e.SerialNumber).HasDefaultValueSql("0");

                entity.Property(e => e.ShortLink).HasMaxLength(50);

                entity.Property(e => e.SocialMediaPrefix).HasMaxLength(150);

                entity.Property(e => e.SplashDescription).HasMaxLength(150);

                entity.Property(e => e.SplashPicture).HasMaxLength(150);

                entity.Property(e => e.SplashTitle).HasMaxLength(50);

                entity.Property(e => e.UltimateIdType).HasDefaultValueSql("0");

                entity.Property(e => e.UltimateLink).HasMaxLength(150);

                entity.Property(e => e.UltimatePicture).HasMaxLength(150);

                entity.Property(e => e.UltimateText).HasMaxLength(50);
            });

            modelBuilder.Entity<TblOndoAdminRelation>(entity =>
            {
                entity.ToTable("tblOndoAdminRelation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AdminRights).HasDefaultValueSql("0");

                entity.Property(e => e.OndoId).HasDefaultValueSql("0");

                entity.Property(e => e.Role).HasDefaultValueSql("0");

                entity.Property(e => e.UserId).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblOndoLottery>(entity =>
            {
                entity.ToTable("tblOndoLottery");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Conditions).HasMaxLength(300);

                entity.Property(e => e.Picture).HasMaxLength(300);

                entity.Property(e => e.SocialMediaPicture).HasMaxLength(300);

                entity.Property(e => e.Text).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.WinnerText).HasMaxLength(300);
            });

            modelBuilder.Entity<TblOndoLotteryRelation>(entity =>
            {
                entity.HasKey(e => e.OndoLotteryRelationId)
                    .HasName("PrimaryKey_dbdda299-2472-4a9d-a23c-0b55b83521a2");

                entity.ToTable("tblOndoLotteryRelation");

                entity.Property(e => e.Month).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblOndoRelation>(entity =>
            {
                entity.HasKey(e => e.RelationId)
                    .HasName("PrimaryKey_a13364c9-07b3-4ec7-b5c4-a2b8215eafa4");

                entity.ToTable("tblOndoRelation");
            });

            modelBuilder.Entity<TblOndoRestaurantRelation>(entity =>
            {
                entity.ToTable("tblOndoRestaurantRelation");

                entity.Property(e => e.OndoId).HasDefaultValueSql("-1");

                entity.Property(e => e.RestaurantIdPoll).HasMaxLength(50);
            });

            modelBuilder.Entity<TblOndos>(entity =>
            {
                entity.HasKey(e => e.OndoId)
                    .HasName("PrimaryKey_8bbffa8b-6b41-4ecf-bfa6-8b3d839a344c");

                entity.ToTable("tblOndos");

                entity.Property(e => e.ActionByte).HasDefaultValueSql("0");

                entity.Property(e => e.AppId).HasDefaultValueSql("0");

                entity.Property(e => e.Barcode).HasMaxLength(50);

                entity.Property(e => e.BarcodeFormat).HasMaxLength(50);

                entity.Property(e => e.CategoryByte).HasDefaultValueSql("0");

                entity.Property(e => e.Content).HasMaxLength(200);

                entity.Property(e => e.ContentType).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(320);

                entity.Property(e => e.Device).HasMaxLength(50);

                entity.Property(e => e.GeoTag).HasMaxLength(150);

                entity.Property(e => e.Link).HasMaxLength(200);

                entity.Property(e => e.Locality).HasMaxLength(50);

                entity.Property(e => e.MapIcon).HasMaxLength(50);

                entity.Property(e => e.MapTag).HasMaxLength(20);

                entity.Property(e => e.MiniPicture).HasMaxLength(200);

                entity.Property(e => e.NewsLetterPicture).HasMaxLength(200);

                entity.Property(e => e.OriginalMiniPicture).HasMaxLength(200);

                entity.Property(e => e.ParentType).HasDefaultValueSql("0");

                entity.Property(e => e.ProfileName).HasMaxLength(50);

                entity.Property(e => e.ProfilePicture).HasMaxLength(150);

                entity.Property(e => e.Qrurl)
                    .HasColumnName("QRUrl")
                    .HasMaxLength(200);

                entity.Property(e => e.Sponsor)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("-1");

                entity.Property(e => e.Status).HasDefaultValueSql("1");

                entity.Property(e => e.StatusMessage).HasMaxLength(150);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<TblPasswordRequest>(entity =>
            {
                entity.ToTable("tblPasswordRequest");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppName).HasMaxLength(50);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Guid).HasMaxLength(50);
            });

            modelBuilder.Entity<TblPayment>(entity =>
            {
                entity.ToTable("tblPayment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmountCharge).HasDefaultValueSql("-1");

                entity.Property(e => e.ReceiptMessage).HasMaxLength(70);
            });

            modelBuilder.Entity<TblPaymentTransaction>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PrimaryKey_362a9eea-d7ff-4dbd-9ffb-f798013a91dd");

                entity.ToTable("tblPaymentTransaction");

                entity.Property(e => e.Amount).HasMaxLength(50);

                entity.Property(e => e.BenefitId).HasDefaultValueSql("-1");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.DbdateTime)
                    .HasColumnName("DBDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dbstatus)
                    .HasColumnName("DBStatus")
                    .HasMaxLength(50);

                entity.Property(e => e.ErrorCode).HasDefaultValueSql("-1");

                entity.Property(e => e.IsSynchronized).HasDefaultValueSql("0");

                entity.Property(e => e.MerchantId).HasMaxLength(50);

                entity.Property(e => e.ReceiptMessage).HasMaxLength(80);

                entity.Property(e => e.TransactionId).HasMaxLength(100);
            });

            modelBuilder.Entity<TblPlannedPosts>(entity =>
            {
                entity.ToTable("tblPlannedPosts");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BenefitId).HasDefaultValueSql("0");

                entity.Property(e => e.CategoryByte).HasDefaultValueSql("0");

                entity.Property(e => e.Community).HasDefaultValueSql("0");

                entity.Property(e => e.ContHeight).HasDefaultValueSql("0");

                entity.Property(e => e.ContWidth).HasDefaultValueSql("0");

                entity.Property(e => e.Content).HasMaxLength(150);

                entity.Property(e => e.Editable).HasDefaultValueSql("1");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IsSent).HasDefaultValueSql("0");

                entity.Property(e => e.MiniPicture).HasMaxLength(150);

                entity.Property(e => e.OndoId).HasDefaultValueSql("0");

                entity.Property(e => e.PostId).HasDefaultValueSql("0");

                entity.Property(e => e.PostType).HasDefaultValueSql("0");

                entity.Property(e => e.SocialMedia).HasDefaultValueSql("0");

                entity.Property(e => e.SocialMediaString).HasMaxLength(280);

                entity.Property(e => e.SocialStatement).HasDefaultValueSql("0");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Text).HasMaxLength(300);

                entity.Property(e => e.Type).HasDefaultValueSql("0");

                entity.Property(e => e.UserId).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblPostNotifications>(entity =>
            {
                entity.ToTable("tblPostNotifications");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasDefaultValueSql("0");

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPushChannels>(entity =>
            {
                entity.ToTable("tblPushChannels");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppId).HasDefaultValueSql("-1");

                entity.Property(e => e.PushUri).HasMaxLength(300);
            });

            modelBuilder.Entity<TblPushNotificationStats>(entity =>
            {
                entity.ToTable("tblPushNotificationStats");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Failed).HasDefaultValueSql("0");

                entity.Property(e => e.Total).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblReservedTransactions>(entity =>
            {
                entity.ToTable("tblReservedTransactions");

                entity.Property(e => e.BenefitId).HasDefaultValueSql("0");

                entity.Property(e => e.CardNr).HasMaxLength(20);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.OndoId).HasDefaultValueSql("0");

                entity.Property(e => e.OrderId).HasDefaultValueSql("-1");

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Points).HasDefaultValueSql("0");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.UserId).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblRssPosts>(entity =>
            {
                entity.ToTable("tblRssPosts");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Rssid)
                    .HasColumnName("RSSId")
                    .HasMaxLength(250);

                entity.Property(e => e.StoryUrl).HasMaxLength(250);
            });

            modelBuilder.Entity<TblSettlements>(entity =>
            {
                entity.ToTable("tblSettlements");

                entity.Property(e => e.ActivityId).HasDefaultValueSql("0");

                entity.Property(e => e.ClubOndoId).HasDefaultValueSql("0");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.OndoId).HasDefaultValueSql("0");

                entity.Property(e => e.Points).HasDefaultValueSql("0");

                entity.Property(e => e.Quarter).HasDefaultValueSql("0");

                entity.Property(e => e.Year).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblSettlementsMonth>(entity =>
            {
                entity.ToTable("tblSettlementsMonth");

                entity.Property(e => e.ActivityId)
                    .IsRequired()
                    .HasColumnType("nchar(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ClubOndoId).HasDefaultValueSql("0");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Month).HasDefaultValueSql("0");

                entity.Property(e => e.OndoId).HasDefaultValueSql("0");

                entity.Property(e => e.Points).HasDefaultValueSql("0");

                entity.Property(e => e.Year).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblSms>(entity =>
            {
                entity.ToTable("tblSMS");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TblSocialMedia>(entity =>
            {
                entity.HasKey(e => e.SocialId)
                    .HasName("PrimaryKey_b39e65c6-5755-43e7-8441-3f18934a6232");

                entity.ToTable("tblSocialMedia");

                entity.Property(e => e.AccessSecret).HasMaxLength(300);

                entity.Property(e => e.AccessToken).HasMaxLength(300);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Expires).HasColumnType("datetime");

                entity.Property(e => e.PageId).HasMaxLength(50);

                entity.Property(e => e.Reason).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<TblStatusInformation>(entity =>
            {
                entity.ToTable("tblStatusInformation");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Level).HasDefaultValueSql("0");

                entity.Property(e => e.OndoId).HasDefaultValueSql("-1");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<TblTrace>(entity =>
            {
                entity.ToTable("tblTrace");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.TraceMinutes).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblUserBalance>(entity =>
            {
                entity.ToTable("tblUserBalance");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Balance).HasDefaultValueSql("0");

                entity.Property(e => e.BarCodePicture).HasMaxLength(150);

                entity.Property(e => e.BenefitId).HasDefaultValueSql("0");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblUserBalanceCard>(entity =>
            {
                entity.ToTable("tblUserBalanceCard");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActivityId).HasDefaultValueSql("-1");

                entity.Property(e => e.BarCodePicture).HasMaxLength(150);

                entity.Property(e => e.CardNr).HasMaxLength(20);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Message).HasMaxLength(300);

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.StatusByte).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<TblUserEvent>(entity =>
            {
                entity.HasKey(e => e.EventIndex)
                    .HasName("PrimaryKey_16693f5d-c775-497c-91d2-e61e3a0f8cab");

                entity.ToTable("tblUserEvent");

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblUserGameStatus>(entity =>
            {
                entity.HasKey(e => e.GameId)
                    .HasName("PrimaryKey_cb30fb45-dcee-44d7-b188-fb7c85f67b13");

                entity.ToTable("tblUserGameStatus");

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblUserLocation>(entity =>
            {
                entity.ToTable("tblUserLocation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblUserSetlementTransactions>(entity =>
            {
                entity.ToTable("tblUserSetlementTransactions");

                entity.Property(e => e.ActivityId).HasDefaultValueSql("0");

                entity.Property(e => e.AdminId).HasDefaultValueSql("0");

                entity.Property(e => e.Balance).HasDefaultValueSql("0");

                entity.Property(e => e.BenefitId).HasDefaultValueSql("0");

                entity.Property(e => e.CardNr).HasMaxLength(50);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.OndoId).HasDefaultValueSql("0");

                entity.Property(e => e.Points).HasDefaultValueSql("0");

                entity.Property(e => e.Reference).HasMaxLength(50);

                entity.Property(e => e.Text).HasMaxLength(50);

                entity.Property(e => e.UserId).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblUserSettlementBalance>(entity =>
            {
                entity.ToTable("tblUserSettlementBalance");

                entity.Property(e => e.Balance).HasDefaultValueSql("0");

                entity.Property(e => e.BenefitId).HasDefaultValueSql("0");

                entity.Property(e => e.CardNr).HasMaxLength(50);

                entity.Property(e => e.UserId).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblUserStatistics>(entity =>
            {
                entity.ToTable("tblUserStatistics");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Action).HasDefaultValueSql("0");

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblUserStatus>(entity =>
            {
                entity.HasKey(e => e.UserStatusIndex)
                    .HasName("PrimaryKey_453e32cf-0e38-4f5a-b63a-a20818dcb471");

                entity.ToTable("tblUserStatus");

                entity.Property(e => e.AppId).HasDefaultValueSql("-1");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.LoyaltyScore).HasDefaultValueSql("2");
            });

            modelBuilder.Entity<TblUserTransactions>(entity =>
            {
                entity.ToTable("tblUserTransactions");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActivityId).HasDefaultValueSql("0");

                entity.Property(e => e.BenefitId).HasDefaultValueSql("0");

                entity.Property(e => e.CardNr).HasMaxLength(50);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.OndoId).HasDefaultValueSql("0");

                entity.Property(e => e.Points).HasDefaultValueSql("0");

                entity.Property(e => e.Reference).HasMaxLength(50);

                entity.Property(e => e.Text).HasMaxLength(280);

                entity.Property(e => e.UserId).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblUserVerification>(entity =>
            {
                entity.ToTable("tblUserVerification");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CardNr).HasMaxLength(20);

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.RunVerification).HasDefaultValueSql("0");

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblUserVouchers>(entity =>
            {
                entity.ToTable("tblUserVouchers");

                entity.Property(e => e.AppId).HasDefaultValueSql("5");

                entity.Property(e => e.BenefitId).HasDefaultValueSql("-1");

                entity.Property(e => e.DueDateTime).HasColumnType("datetime");

                entity.Property(e => e.Information).HasDefaultValueSql("0");

                entity.Property(e => e.IssueReason).HasDefaultValueSql("0");

                entity.Property(e => e.IssuedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.TriggerDateTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasDefaultValueSql("-1");
            });

            modelBuilder.Entity<TblUsers>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PrimaryKey_86affb68-c8e5-4626-ae7f-44fb1cc027a8");

                entity.ToTable("tblUsers");

                entity.Property(e => e.AccessToken).HasMaxLength(300);

                entity.Property(e => e.ActionByte).HasDefaultValueSql("0");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Culture).HasMaxLength(10);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.IsVirtual).HasDefaultValueSql("0");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.PhonePrefix).HasMaxLength(5);

                entity.Property(e => e.Picture).HasMaxLength(300);

                entity.Property(e => e.PushUri).HasMaxLength(300);

                entity.Property(e => e.Subscription).HasDefaultValueSql("0");

                entity.Property(e => e.Token).HasMaxLength(150);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblUsersToWelcome>(entity =>
            {
                entity.ToTable("tblUsersToWelcome");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblVirtualUsers>(entity =>
            {
                entity.ToTable("tblVirtualUsers");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FromUserId).HasDefaultValueSql("0");

                entity.Property(e => e.InfoByte).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TblVoucherPrograms>(entity =>
            {
                entity.HasKey(e => e.VoucherProgramId)
                    .HasName("PK__Table__12EE2F91B831210F");

                entity.ToTable("tblVoucherPrograms");

                entity.Property(e => e.ActionByte).HasDefaultValueSql("0");

                entity.Property(e => e.AppId).HasDefaultValueSql("5");

                entity.Property(e => e.BenefitId).HasDefaultValueSql("-1");

                entity.Property(e => e.Information).HasDefaultValueSql("0");

                entity.Property(e => e.MaxUse).HasDefaultValueSql("-1");

                entity.Property(e => e.OndoId).HasDefaultValueSql("-1");

                entity.Property(e => e.Status).HasDefaultValueSql("-1");

                entity.Property(e => e.Type).HasDefaultValueSql("0");

                entity.Property(e => e.Validity).HasMaxLength(50);
            });
        }
    }
}