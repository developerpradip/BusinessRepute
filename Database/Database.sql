/****** Object:  StoredProcedure [dbo].[BUSINESS_DETAILS_S]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BUSINESS_DETAILS_S]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[BUSINESS_DETAILS_S] 
	@userId int =null
	,@businessRoleId smallint =null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')

		--[BusinessDetails]
		select 	[businessName]
					,[logoName]
					,[logoFileName]
					,[businessAddress1]
					,[businessAddress2]
					,[businessEmailId]
					,[businessPhone]
					,[businessFax]
					,[businessCity]
					,[businessState]
					,[businesszip]
					,[businessTypeId]
					,BD.userId
					,SecurityQ1
					,SecurityA1
					,SecurityQ2
					,SecurityA2
					,BD.businessDetailId  as businessDetailId

				From [dbo].[BusinessDetails] BD, UserSecurityDetails USD, BusinessUserRole BUR, UserDetails UD, PersonalDetail PD
				WHERE 
					BD.businessDetailId= BUR.BusinessDetailid
					and USD.UserSecurityDetailId = PD.userSecuritydetailId
					and PD.userSecuritydetailId=UD.PersonalDetailId
					and BUR.UserID = UD.UserId 
					and BUR.BusinessRoleId=COALESCE(@businessRoleId, 1)
					and BUR.UserID =@userId
				
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100



' 
END
GO
/****** Object:  StoredProcedure [dbo].[CHECK_DUPLICATE_USERNAME]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CHECK_DUPLICATE_USERNAME]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE  PROCEDURE [dbo].[CHECK_DUPLICATE_USERNAME] 
	@USER_UserName varchar (25) 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')


    
	SELECT userid from UserDetails where 
	UserName = @USER_UserName 
	
	SELECT	@SQLRowcount = @@ROWCOUNT,
		@SQLError = @@ERROR
	
IF @SQLError <> 0
	GOTO EXIT_ERROR
	
END


-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100

' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BUSINESS_INSERT_UPDATE]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_BUSINESS_INSERT_UPDATE]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_BUSINESS_INSERT_UPDATE] 
	@BussName varchar (50),
	@BussLogoName varchar (25) =null,
	@BussLogoFileName varchar (55) =null,
	@BussAddress1 varchar (100),
	@BussAddress2 varchar (100),
	@BussEmailId varchar (50),
	@BussPhoneNumber varchar (25),
	@BussFaxNumber varchar (25),
	@Mode smallint =0,
	@userId smallint =null,
	@businessDetailId smallint =null,
	@BussCity varchar (25) = null,
	@BussState varchar (25) = null,
	@BussZipCode varchar (10) = null,
	@BusinessTypeId int = -1,
	@ReturnBusinessDetailId int out 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')

	if @Mode = 0
		BEGIN
			--[BusinessDetails]
			INSERT INTO [dbo].[BusinessDetails]
						([businessName]
						,[logoName]
						,[logoFileName]
						,[businessAddress1]
						,[businessAddress2]
						,[businessEmailId]
						,[businessPhone]
						,[businessFax]
						,[userId]
						,[businessCity]
						,[businessState]
						,[businessZip]
						,[BusinessTypeId])

					VALUES
						(@BussName
						,@BussLogoName
						,@BussLogoFileName
						,@BussAddress1
						,@BussAddress2
						,@BussEmailId
						,@BussPhoneNumber
						,@BussFaxNumber
						,@userId
						,@BussCity
						,@BussState 
						,@BussZipCode
						,@BusinessTypeId )
				

				SELECT	@SQLRowcount = @@ROWCOUNT, @SQLError = @@ERROR , @ReturnBusinessDetailId = SCOPE_IDENTITY()
				IF @SQLError <> 0
					GOTO EXIT_ERROR

				
				select @ReturnBusinessDetailId
			END
			--update the user details record
		if @Mode = 1
			
			BEGIN
				SET NOCOUNT OFF;
				UPDATE [dbo].[BusinessDetails] SET 
						[businessName]=@BussName
						
						,[businessAddress1]=@BussAddress1
						,[businessAddress2]=@BussAddress2
						,[businessEmailId]=@BussEmailId
						,[businessPhone]=@BussPhoneNumber
						,[businessFax]=@BussFaxNumber
						,[businessCity] =@BussCity
						,[businessState] = @BussState
						,[businessZip]=@BussZipCode 
						,[BusinessTypeId] = @BusinessTypeId
					where [businessDetailId] =@businessDetailId

				SELECT	@SQLRowcount = @@ROWCOUNT, @SQLError = @@ERROR 
				IF @SQLError <> 0
					GOTO EXIT_ERROR

					select @ReturnBusinessDetailId = @businessDetailId
					select @SQLRowcount
			END
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100


' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BUSINESSID_FROM_USERID_S]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_BUSINESSID_FROM_USERID_S]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_BUSINESSID_FROM_USERID_S] 
	@userId int =null,
	@businessRoleId int = null
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')

		--[BusinessDetails]
		select 	BD.businessDetailId  as businessDetailId

				From [dbo].[BusinessDetails] BD, UserSecurityDetails USD, BusinessUserRole BUR, UserDetails UD, PersonalDetail PD
				WHERE 
					BD.businessDetailId= BUR.BusinessDetailid
					and USD.UserSecurityDetailId = PD.userSecuritydetailId
					and PD.userSecuritydetailId=UD.PersonalDetailId
					and BUR.UserID = UD.UserId 
					and BUR.BusinessRoleId=COALESCE(@businessRoleId, 1)
					and BUR.UserID =@userId
				
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BusinessRole_S]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_BusinessRole_S]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_BusinessRole_S] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')


    
	SELECT BusinessRoleName , BusinessRoleId from BusinessRole 
	where active=1 and BusinessRoleId>1
	
	SELECT	@SQLError = @@ERROR,
	@SQLRowcount = @@ROWCOUNT

IF @SQLError <> 0
	GOTO EXIT_ERROR
	
END


-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100


' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BusinessSettings_D]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_BusinessSettings_D]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_BusinessSettings_D] 
	@businessDetailId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')

		--[BusinessDetails]
			Delete from BusinessSettings
			WHERE 
			businessDetailId= @businessDetailId
					
					
				
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BusinessSettings_IU]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_BusinessSettings_IU]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_BusinessSettings_IU] 

	@BusinessDetailId int,
	@TTTypeID int,
	@MinTTLength int =0,
	@MaxTTLength int =0,
	@DefaultTTFee float (15),
	@DefaultDiscSinglePayPer float (15),
	@MinMonthlyPayEMI float (15),
	@MinMonthlyPayDownPayAmt float (15),
	@MinNoDownPayEMI float (15),
	@other varchar (200) = null,
	@TTname varchar (50) = null,
	@ShowInvBraceComparison smallint = 0,
	@ShowOtherPaymentOption smallint = 0


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')
	DECLARE @BusinessSettingId int

	select @BusinessSettingId = BusinessSettingId 
		from BusinessSettings
		where BusinessDetailId  = @BusinessDetailId 
		and TreatmentTypeId = @TTTypeID 
	SET NOCOUNT OFF;
	if @BusinessSettingId > 0
		begin
			update BusinessSettings
				   set MinTTLength = @MinTTLength
				   ,MaxTTLength = @MaxTTLength
				   ,TTDefaultFee = @DefaultTTFee
				   ,DefaultDiscSinglePayPer = @DefaultDiscSinglePayPer
				   ,MinMonthlyPayEMI = @MinMonthlyPayEMI
				   ,MinMonthlyPayDownPayAmt = @MinMonthlyPayDownPayAmt
				   ,MinNoDownPayEMI = @MinNoDownPayEMI
				   ,other= @other
				   ,TTName= @TTName
				   ,ShowInvBraceComparison = @ShowInvBraceComparison
				   ,OptOtherPaymentOption = @ShowOtherPaymentOption
				where BusinessDetailId  = @BusinessDetailId 
					and TreatmentTypeId = @TTTypeID 

				select @@ROWCOUNT
		end
	else
		begin
				INSERT INTO dbo.BusinessSettings
				   (BusinessDetailId
				   ,TreatmentTypeId
				   ,MinTTLength
				   ,MaxTTLength
				   ,TTDefaultFee
				   ,DefaultDiscSinglePayPer
				   ,MinMonthlyPayEMI
				   ,MinMonthlyPayDownPayAmt
				   ,MinNoDownPayEMI
				   ,other
				   ,TTName
				   ,ShowInvBraceComparison
				   ,OptOtherPaymentOption)
			 VALUES
				   (@BusinessDetailId,
					@TTTypeID,
					@MinTTLength,
					@MaxTTLength,
					@DefaultTTFee,
					@DefaultDiscSinglePayPer ,
					@MinMonthlyPayEMI ,
					@MinMonthlyPayDownPayAmt ,
					@MinNoDownPayEMI ,
					@other,
					@TTName,
					@ShowInvBraceComparison,
					@ShowOtherPaymentOption)

			select @@ROWCOUNT
		end
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BusinessSettings_S]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_BusinessSettings_S]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_BusinessSettings_S] 
	@businessDetailId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')

		--[BusinessDetails]
		SELECT  [BusinessSettingId]
					  ,[BusinessDetailId]
					  ,BS.[TreatmentTypeId] as TreatmentTypeId
					  ,[TreatmentName] 
					  ,[MinTTLength]
					  ,[MaxTTLength]
					  ,[TTDefaultFee]
					  ,[DefaultDiscSinglePayPer]
					  ,[MinMonthlyPayEMI]
					  ,[MinMonthlyPayDownPayAmt]
					  ,[MinNoDownPayEMI]
					  ,[other]
					  ,[TTName] as TTCustomName
					  ,[ShowInvBraceComparison] 
					  ,OptOtherPaymentOption 

			FROM [BusinessSettings] BS, [TreatmentType] TT
			WHERE 
					businessDetailId= @businessDetailId
					and BS.TreatmentTypeId = TT.TreatmentTypeId
					
				
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BusinessType_S]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_BusinessType_S]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_BusinessType_S] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')

		--[BusinessType]
		select 	BusinessTypeId 
				,BusinessType 
				,Description 
				,Active 
			From [BusinessType] 
			WHERE 
				Active =1
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BusinessUserRole_IU]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_BusinessUserRole_IU]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_BusinessUserRole_IU] 

		@BusinessRoleId int,
        @UserId int,
        @BusinessDetailId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')
	declare @BusinessUserRoleId int	set @BusinessUserRoleId =0

	select @BusinessUserRoleId =BusinessUserRoleId
		from BusinessUserRole
		where BusinessRoleId = @BusinessRoleId and UserId = @UserId and BusinessDetailid = @BusinessDetailId

		
	if @BusinessUserRoleId = null or @BusinessUserRoleId = 0 or @BusinessUserRoleId = '''' 
		BEGIN 
			INSERT INTO [dbo].[BusinessUserRole]
					   (BusinessRoleId
					   ,UserId
					   ,[BusinessDetailid])
				 VALUES
					   (@BusinessRoleId ,
						@UserId,
						@BusinessDetailId)
		end
		else
		begin
			update [dbo].[BusinessUserRole]
				set BusinessRoleId = @BusinessRoleId, UserId = @UserId, [BusinessDetailid] = @BusinessDetailId
				where BusinessUserRoleId = @BusinessUserRoleId
		end
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BusinessUsers_S]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_BusinessUsers_S]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_BusinessUsers_S] 
	@BusinessDetailId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')


    
	select BusinessUserRoleId, UD.UserId UserId, BusinessDetailId, Username, Firstname, Lastname, EmailId, 
			PhonenUmber, BusinessRoleName, BR.BusinessRoleId BusinessRoleId
			from [dbo].[BusinessUserRole] BUR, UserDetails UD, PersonalDetail PD, BusinessRole BR
			where BUR.UserID = UD.UserId
		and PD.PersonalDetailId = UD.PersonalDetailId
		and BR.BusinessRoleId= BUr.BusinessRoleId
		and BusinessDetailid =@BusinessDetailid 
		and BUR.Active=1
		and BUR.BusinessRoleId >1
	
	SELECT	@SQLError = @@ERROR,
	@SQLRowcount = @@ROWCOUNT

IF @SQLError <> 0
	GOTO EXIT_ERROR
	
END


-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100


' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_FeedbackDetails_I]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_FeedbackDetails_I]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_FeedbackDetails_I] 
	@name varchar(50) =null,
	@email varchar(50) =null,
	@subject varchar(50) =null,
	@message varchar(2000) =null,
	@purpose varchar(100) =null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')

	
	--[FeedbackDetails]
	Insert into FeedbackDetails (Name, email, subject, message, purpose) 
	values (@name, @email, @subject, @message, @purpose)
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_TREATMENTTYPE_NAME]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GET_TREATMENTTYPE_NAME]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GET_TREATMENTTYPE_NAME] 
	@ttType smallint
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')

		--[BusinessSettings]
		select TTname from  BusinessSettings
		where TreatmentTypeId= @ttType

		SELECT	@SQLError = @@ERROR,
		@SQLRowcount = @@ROWCOUNT

		IF @SQLError <> 0
			GOTO EXIT_ERROR
	
				
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_TREATMENTTYPEID]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GET_TREATMENTTYPEID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GET_TREATMENTTYPEID] 
	@ttType varchar (25) =null
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')

		--[BusinessSettings]
		select TreatmentTypeId from  BusinessSettings
		where TTname = @ttType

		SELECT	@SQLError = @@ERROR,
		@SQLRowcount = @@ROWCOUNT

		IF @SQLError <> 0
			GOTO EXIT_ERROR
	
				
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Status_S]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_Status_S]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_Status_S]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')

	SELECT STATUSID, STATUSNAME 
	FROM STATUS
	WHERE ACTIVE=1 order by PLSource
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_USER_INFO_D]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_USER_INFO_D]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_USER_INFO_D] 
	@userId int 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')
	DECLARE @returnBusinessDetailId int

	
		update BusinessUserRole 
			set Active =0 
			where userid=@userId

		update UserDetails 
			set userstatus =0 
			where userid=@userId
		
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100


' 
END
GO
/****** Object:  StoredProcedure [dbo].[USER_INSERT_UPDATE]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USER_INSERT_UPDATE]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[USER_INSERT_UPDATE] 
	@USER_UserName varchar (25) = null, 
	@Password varchar (25) = null,
	@FirstName varchar (25) =null,
	@LastName varchar (25)=null,
	@EmailId varchar (50) =null,
	@PhoneNumber varchar (25)=null,
	@SecurityQ1 varchar (50)=null,
	@SecurityA1 varchar (50)=null,
	@SecurityQ2 varchar (50)=null,
	@SecurityA2 varchar (50)=null,
	@BussName varchar (50),
	@BussLogoName varchar (25)=null,
	@BussLogoFileName varchar (55)=null,
	@BussAddress1 varchar (100),
	@BussAddress2 varchar (100),
	@BussEmailId varchar (50),
	@BussPhoneNumber varchar (25),
	@BussFaxNumber varchar (25),
	@Mode smallint =0,
	@ActivationCode varchar (50) = null,
	@userId smallint =null,
	@businessDetailId smallint =null,
	@BussCity varchar (25) = null,
	@BussState varchar (25) = null,
	@BussZipCode varchar (10) = null,
	@BussRoleID int= -1,
	@BusinessTypeId int = -1

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')
	DECLARE @returnBusinessDetailId int

	if @Mode = 0
		BEGIN
			BEGIN TRANSACTION
				--UserSecurityDetails
				INSERT UserSecurityDetails
					(SecurityQ1, SecurityA1,SecurityQ2, SecurityA2)
				VALUES
					(@SecurityQ1, @SecurityA1,@SecurityQ2, @SecurityA2)

				SELECT	@SQLError = @@ERROR, @SQLRowcount = @@ROWCOUNT,	@SQLIdentity = SCOPE_IDENTITY()
				IF @SQLError <> 0
					GOTO EXIT_ERROR

				--PersonalDetail
				INSERT PersonalDetail
					(FirstName, LastName, EmailId,PhoneNumber, userSecuritydetailId)
				VALUES
					(@FirstName, @LastName, @EmailId, @PhoneNumber, @SQLIdentity)

				
	
				SELECT	@SQLError = @@ERROR, @SQLRowcount = @@ROWCOUNT,	@SQLIdentity = SCOPE_IDENTITY()
				IF @SQLError <> 0
					GOTO EXIT_ERROR

				SET NOCOUNT OFF;
				
				--UserDetails
				INSERT UserDetails
					(UserName, Password, PersonalDetailId, userCode, userCodeType, userstatus)
				VALUES
					(@USER_UserName, @Password, @SQLIdentity, @ActivationCode, 1, 2)

				
				SELECT	@SQLRowcount = @@ROWCOUNT, @SQLError = @@ERROR, @SQLIdentity = SCOPE_IDENTITY()
				IF @SQLError <> 0
					GOTO EXIT_ERROR
				
				set @userId=@SQLIdentity
				
				--business details
				Exec [SP_BUSINESS_INSERT_UPDATE] @BussName, @BussLogoName, @BussLogoFileName, @BussAddress1, @BussAddress2 
					,@BussEmailId, @BussPhoneNumber, @BussFaxNumber, @Mode ,@userId ,@businessDetailId, @BussCity,	
					@BussState,	@BussZipCode, @BusinessTypeId, @returnBusinessDetailId OUTPUT 

				SELECT	@SQLRowcount = @@ROWCOUNT, @SQLError = @@ERROR
				IF @SQLError <> 0
					GOTO EXIT_ERROR

				Exec SP_BusinessUserRole_IU @BussRoleID, @userId, @returnBusinessDetailId 

				COMMIT TRANSACTION
				return @SQLRowcount
			END
			--update the user details record
		if @Mode = 1
			BEGIN
				DECLARE @PersonalDetailId INT
				DECLARE @userSecuritydetailId INT

				select @PersonalDetailId = ud.PersonalDetailId, @userSecuritydetailId= userSecuritydetailId
					from UserDetails UD, PersonalDetail PD
					where UD.UserId=@userId 
					and UD.PersonalDetailId = PD.PersonalDetailId

				BEGIN TRANSACTION
					--UserSecurityDetails
					UPDATE UserSecurityDetails
						SET SecurityQ1=@SecurityQ1, 
						SecurityA1 = @SecurityA1,
						SecurityQ2=@SecurityQ2, 
						SecurityA2 = @SecurityA2
						WHERE userSecuritydetailId= @userSecuritydetailId

					SELECT	@SQLRowcount = @@ROWCOUNT, @SQLError = @@ERROR 
					IF @SQLError <> 0
						GOTO EXIT_ERROR
					
					SET NOCOUNT OFF;
				
					--PersonalDetail
					UPDATE PersonalDetails
						SET FirstName = @FirstName, 
						LastName = @LastName,
						EmailId = @EmailId,
						PhoneNumber = @PhoneNumber
						WHERE PersonalDetailId = @PersonalDetailId
				
					SELECT	@SQLRowcount = @@ROWCOUNT, @SQLError = @@ERROR
					IF @SQLError <> 0
						GOTO EXIT_ERROR

				
				--business details
				Exec [SP_BUSINESS_INSERT_UPDATE] @BussName, @BussLogoName ,@BussLogoFileName ,@BussAddress1 ,@BussAddress2 
					,@BussEmailId ,@BussPhoneNumber ,@BussFaxNumber ,@Mode ,@userId ,@businessDetailId, @BussCity,	
					@BussState,	@BussZipCode, @BusinessTypeId, @returnBusinessDetailId OUTPUT 

				SELECT	@SQLRowcount = @@ROWCOUNT, @SQLError = @@ERROR
				IF @SQLError <> 0
					GOTO EXIT_ERROR

				Exec SP_BusinessUserRole_IU @BussRoleID, @userId, @returnBusinessDetailId 

				COMMIT TRANSACTION
				RETURN @SQLRowcount
			END
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100


' 
END
GO
/****** Object:  StoredProcedure [dbo].[USER_IU]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USER_IU]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[USER_IU] 
	@USER_UserName varchar (25) = null, 
	@Password varchar (25) = null,
	@FirstName varchar (25) ,
	@LastName varchar (25),
	@EmailId varchar (50),
	@PhoneNumber varchar (25),
	@SecurityQ1 varchar (50),
	@SecurityA1 varchar (50),
	@SecurityQ2 varchar (50),
	@SecurityA2 varchar (50),
	@profilePicName varchar (25),
	@profilePicFileName varchar (255),
	@Mode smallint =0,
	@ActivationCode varchar (50) = null,
	@userId int = null,
	@BussRoleId int = null,
	@BusinessDetailsID int =null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')
	DECLARE @returnBusinessDetailId int

	if @Mode = 0
		BEGIN
			BEGIN TRANSACTION
				--UserSecurityDetails
				INSERT UserSecurityDetails
					(SecurityQ1, SecurityA1,SecurityQ2, SecurityA2)
				VALUES
					(@SecurityQ1, @SecurityA1,@SecurityQ2, @SecurityA2)

				SELECT	@SQLError = @@ERROR, @SQLRowcount = @@ROWCOUNT,	@SQLIdentity = SCOPE_IDENTITY()
				IF @SQLError <> 0
					GOTO EXIT_ERROR

				--PersonalDetail
				INSERT PersonalDetail
					(FirstName, LastName, EmailId, PhoneNumber, profilePicName, profilePicFileName, userSecuritydetailId)
				VALUES
					(@FirstName, @LastName, @EmailId, @PhoneNumber, @profilePicName, @profilePicFileName, @SQLIdentity)

				SELECT	@SQLError = @@ERROR, @SQLRowcount = @@ROWCOUNT,	@SQLIdentity = SCOPE_IDENTITY()
				IF @SQLError <> 0
					GOTO EXIT_ERROR

				SET NOCOUNT OFF;
				--UserDetails
				INSERT UserDetails
					(UserName, Password, PersonalDetailId, userCode, userCodeType, userstatus)
				VALUES
					(@USER_UserName, @Password, @SQLIdentity, @ActivationCode, 1, 2)

				
				SELECT	@SQLRowcount = @@ROWCOUNT, @SQLError = @@ERROR, @SQLIdentity = SCOPE_IDENTITY()
				IF @SQLError <> 0
					GOTO EXIT_ERROR
				
				set @userId=@SQLIdentity
				
				Exec SP_BusinessUserRole_IU @BussRoleId, @userId, @BusinessDetailsID 

				COMMIT TRANSACTION
				return @SQLRowcount
			END
			--update the user details record
		if @Mode = 1
			BEGIN
				DECLARE @PersonalDetailId INT
				DECLARE @userSecuritydetailId INT

				select @PersonalDetailId = ud.PersonalDetailId, @userSecuritydetailId= userSecuritydetailId
					from UserDetails UD, PersonalDetail PD
					where UD.UserId=@userId 
					and UD.PersonalDetailId = PD.PersonalDetailId
					
					
					print @userId
				BEGIN TRANSACTION
				
					SET NOCOUNT OFF;
				
					--PersonalDetail
					UPDATE PersonalDetail
						SET FirstName = @FirstName, 
						LastName = @LastName,
						EmailId = @EmailId,
						PhoneNumber = @PhoneNumber
						WHERE PersonalDetailId = @PersonalDetailId
					
					SELECT	@SQLRowcount = @@ROWCOUNT, @SQLError = @@ERROR
					IF @SQLError <> 0
						GOTO EXIT_ERROR

						
					UPDATE BusinessUserRole
						SET BusinessRoleId = @BussRoleId
						WHERE UserID = @userId
						and BusinessDetailid = @BusinessDetailsID
				
					SELECT	@SQLRowcount = @@ROWCOUNT, @SQLError = @@ERROR
					IF @SQLError <> 0
						GOTO EXIT_ERROR

				COMMIT TRANSACTION
				RETURN @SQLRowcount
			END
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100


' 
END
GO
/****** Object:  StoredProcedure [dbo].[USER_S]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USER_S]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[USER_S]
	@Userid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')

	select FirstName, LastName, EmailId, PhoneNumber, SecurityQ1, SecurityA1, SecurityQ2, SecurityA2, BusinessRoleId
		from UserDetails UD, personaldetail PD, UserSecurityDetails USD, BusinessUserRole BUR
	where UD.PersonalDetailId=PD.PersonalDetailId 
	and USD.UserSecurityDetailId = PD.userSecuritydetailId
	and BUR.UserID = @userid
	and UD.userid = @userid
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100



' 
END
GO
/****** Object:  StoredProcedure [dbo].[USER_SELECT]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USER_SELECT]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE  PROCEDURE [dbo].[USER_SELECT] 
	@USER_NAME  varchar (50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')


     
	SELECT userid from  UserDetails where 
	
	username=@USER_NAME
	
	SELECT	@SQLError = @@ERROR,
	@SQLRowcount = @@ROWCOUNT

IF @SQLError <> 0
	GOTO EXIT_ERROR
	
END


-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100




' 
END
GO
/****** Object:  StoredProcedure [dbo].[USERDETAILS_ACTIVATION_INSERT_RESET]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USERDETAILS_ACTIVATION_INSERT_RESET]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[USERDETAILS_ACTIVATION_INSERT_RESET] 
	@EmailId varchar (50),
	@ActivationCode varchar (50) = null
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')


	SET NOCOUNT OFF;
	--UserDetails
	UPDATE UserDetails
		SET userCode=@ActivationCode,
		USERCODETYPE=2,
		userstatus=3
	WHERE
		PersonalDetailId=(SELECT top 1 PersonalDetailId FROM PersonalDetail
			WHERE EmailId = @EmailId)
		--and userstatus=1

	SELECT	@SQLError = @@ERROR, @SQLRowcount = @@ROWCOUNT
	IF @SQLError <> 0
		GOTO EXIT_ERROR

	RETURN @SQLRowcount
	
END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100


' 
END
GO
/****** Object:  StoredProcedure [dbo].[USERDETAILS_CHANGE_PASSWORD]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USERDETAILS_CHANGE_PASSWORD]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[USERDETAILS_CHANGE_PASSWORD]
	@userId smallint,
	@OldPassword varchar (50) = null,
	@NewPassword varchar (50) = null,
	@ActivationCode varchar(50) = null
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')

	/*select userid from UserDetails
	where Password = @OldPassword and userid= @userId

	SELECT	@SQLError = @@ERROR, @SQLRowcount = @@ROWCOUNT
	IF @SQLError <> 0
		GOTO EXIT_ERROR
		*/
	if @ActivationCode is null
		begin
			update UserDetails set 
			Password = @NewPassword
			where userid= @userId 
			and Password = @OldPassword
		end
	else
		begin
			update UserDetails set 
			Password = @NewPassword,
			userstatus=1
			where userCode= @ActivationCode 
		
		end 
	SELECT	@SQLError = @@ERROR, @SQLRowcount = @@ROWCOUNT
	IF @SQLError <> 0
		GOTO EXIT_ERROR

END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100


' 
END
GO
/****** Object:  StoredProcedure [dbo].[USERDETAILS_SELECT]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USERDETAILS_SELECT]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[USERDETAILS_SELECT] 
	@USER_UserName varchar (25), 
	@Password varchar (25)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')


	SELECT UD.userid userid, businessDetailId, BusinessRoleId, FirstName 
		from UserDetails UD, BusinessUserRole BUR, PersonalDetail PD
		where UD.PersonalDetailId =PD.PersonalDetailId
		and BUR.UserID = UD.UserId
		and UserName = @USER_UserName 
		and Password=@Password
	

    
	
	
	SELECT	@SQLError = @@ERROR,
	@SQLRowcount = @@ROWCOUNT

IF @SQLError <> 0
	GOTO EXIT_ERROR
	
END


-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100


' 
END
GO
/****** Object:  StoredProcedure [dbo].[USERDETAILS_USER_ACTIONS]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USERDETAILS_USER_ACTIONS]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[USERDETAILS_USER_ACTIONS]
	@userCodeType smallint,
	@code varchar (50) = null
	
AS
BEGIN
	SET LOCK_TIMEOUT 10000 -- 10 seconds (raises SQL error 1222)
    
    DECLARE	@RtnVal int			SET @RtnVal = 0
	DECLARE	@Msg varchar(100)		SET @Msg = CONVERT(varchar(100),'''')
	DECLARE	@SQLError int			SET @SQLError = 0
	DECLARE @SQLRowcount int		SET @SQLRowcount = 0
	DECLARE @SQLIdentity int		SET @SQLIdentity = 0
	DECLARE	@HostName varchar(100)		SET @HostName = HOST_NAME()
	DECLARE	@UserName varchar(100)		SET @UserName = SUSER_SNAME()
	DECLARE	@ProcName varchar(100)		SET @ProcName = OBJECT_NAME(@@PROCID)
	DECLARE	@ProcDateTime datetime		SET @ProcDateTime = GETDATE()
	DECLARE	@EmptyDatetime datetime		SET @EmptyDatetime = CONVERT(datetime,''01/01/1900 00:00:00.000'')
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;
	

	update UserDetails set 
	userstatus= 0 where 
	userCode =@code
	and userCodeType = @userCodeType

	select @@ROWCOUNT

	SELECT	@SQLError = @@ERROR, @SQLRowcount = @@ROWCOUNT
	IF @SQLError <> 0
		GOTO EXIT_ERROR
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

END

-----------------------------------------------------------
--	EXIT AND LOGGING

EXIT_GOOD:
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN 0

EXIT_ERROR:

	WHILE @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		
	--  Set return parameters to "error" value.
	SET LOCK_TIMEOUT -1 -- reset to default (no mandatory time-out period)
	SET NOCOUNT OFF
	RETURN -100


' 
END
GO
/****** Object:  Table [dbo].[BusinessDetails]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BusinessDetails](
	[businessDetailId] [int] IDENTITY(1,1) NOT NULL,
	[businessName] [varchar](50) NULL,
	[logoName] [varchar](25) NULL,
	[logoFileName] [varchar](55) NULL,
	[businessAddress1] [varchar](100) NULL,
	[businessAddress2] [varchar](100) NULL,
	[businessEmailId] [varchar](50) NULL,
	[businessPhone] [varchar](25) NULL,
	[businessFax] [varchar](25) NULL,
	[userId] [int] NULL,
	[businessCity] [varchar](25) NULL,
	[businessState] [varchar](25) NULL,
	[businessZip] [varchar](10) NULL,
	[businessTypeId] [smallint] NOT NULL,
 CONSTRAINT [PK__Business__D8893CE0E29B1FB2] PRIMARY KEY CLUSTERED 
(
	[businessDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BusinessRole]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BusinessRole](
	[BusinessRoleId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessRoleName] [varchar](60) NULL,
	[BusinessRoleDesc] [varchar](100) NULL,
	[Active] [smallint] NULL,
 CONSTRAINT [PK__BusinessRoleId__D8893CE0E29B1FB2] PRIMARY KEY CLUSTERED 
(
	[BusinessRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BusinessSettings]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BusinessSettings](
	[BusinessSettingId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessDetailId] [int] NULL,
	[TreatmentTypeId] [int] NULL,
	[MinTTLength] [int] NULL,
	[MaxTTLength] [int] NULL,
	[TTDefaultFee] [numeric](15, 2) NULL,
	[DefaultDiscSinglePayPer] [numeric](15, 2) NULL,
	[MinMonthlyPayEMI] [numeric](15, 2) NULL,
	[MinMonthlyPayDownPayAmt] [numeric](15, 2) NULL,
	[MinNoDownPayEMI] [numeric](15, 2) NULL,
	[other] [varchar](200) NULL,
	[TTname] [varchar](50) NULL,
	[ShowInvBraceComparison] [smallint] NULL,
	[OptOtherPaymentOption] [smallint] NULL,
 CONSTRAINT [PK__BusinessSettings__D8893CE0E29B1FB2] PRIMARY KEY CLUSTERED 
(
	[BusinessSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BusinessType]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BusinessType](
	[BusinessTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[BusinessType] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[Active] [smallint] NOT NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BusinessUserRole]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessUserRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BusinessUserRole](
	[BusinessUserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessRoleId] [int] NULL,
	[UserID] [int] NULL,
	[BusinessDetailid] [int] NULL,
	[Active] [smallint] NULL,
 CONSTRAINT [PK__BusinessUserRoleId__D8893CE0E29B1FB2] PRIMARY KEY CLUSTERED 
(
	[BusinessUserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[FeedbackDetails]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FeedbackDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FeedbackDetails](
	[FeedbackDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[subject] [varchar](50) NULL,
	[message] [varchar](2000) NULL,
	[purpose] [varchar](100) NULL,
	[FeedbackDate] [datetime] NULL,
 CONSTRAINT [PK__FeedbackDetails__D8893CE0E29B1FB2] PRIMARY KEY CLUSTERED 
(
	[FeedbackDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PersonalDetail]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonalDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PersonalDetail](
	[PersonalDetailId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](25) NULL,
	[LastName] [varchar](25) NULL,
	[EmailId] [varchar](50) NULL,
	[PhoneNumber] [varchar](25) NULL,
	[userSecuritydetailId] [int] NULL,
	[profilePicName] [varchar](25) NULL,
	[profilePicFileName] [varchar](255) NULL,
 CONSTRAINT [PK__Personal__D8893CE0E29B1FB2] PRIMARY KEY CLUSTERED 
(
	[PersonalDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Status]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Status](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[StatusGroup] [char](20) NULL,
	[StatusName] [varchar](60) NULL,
	[Active] [smallint] NULL,
	[PLSource] [smallint] NULL,
 CONSTRAINT [PK__StatusId__D8893CE0E29B1FB2] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TreatmentType]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TreatmentType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TreatmentType](
	[TreatmentTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentName] [varchar](50) NULL,
 CONSTRAINT [PK__TreatmentTypeId__D8893CE0E29B1FB2] PRIMARY KEY CLUSTERED 
(
	[TreatmentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserDetails](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](25) NULL,
	[Password] [varchar](25) NULL,
	[PersonalDetailId] [int] NULL,
	[userstatus] [smallint] NULL,
	[userCode] [varchar](50) NULL,
	[userCodeType] [smallint] NULL,
 CONSTRAINT [PK__UserId__D8893CE0E29B1FB2] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserSecurityDetails]    Script Date: 8/25/2018 12:30:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSecurityDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserSecurityDetails](
	[UserSecurityDetailId] [int] IDENTITY(1,1) NOT NULL,
	[SecurityQ1] [varchar](50) NULL,
	[SecurityA1] [varchar](50) NULL,
	[SecurityQ2] [varchar](50) NULL,
	[SecurityA2] [varchar](50) NULL,
 CONSTRAINT [PK__UserSecu__1C5C189FEA4DB2E9] PRIMARY KEY CLUSTERED 
(
	[UserSecurityDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_BusinessDetails_businessTypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BusinessDetails] ADD  CONSTRAINT [DF_BusinessDetails_businessTypeId]  DEFAULT ((0)) FOR [businessTypeId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__BusinessR__Activ__79FD19BE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BusinessRole] ADD  DEFAULT ((1)) FOR [Active]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__BusinessS__ShowI__5A4F643B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BusinessSettings] ADD  DEFAULT ((0)) FOR [ShowInvBraceComparison]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__BusinessS__OptOt__6B79F03D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BusinessSettings] ADD  DEFAULT ((0)) FOR [OptOtherPaymentOption]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_BusinessType_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BusinessType] ADD  CONSTRAINT [DF_BusinessType_Active]  DEFAULT ((1)) FOR [Active]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__BusinessU__Activ__7AF13DF7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BusinessUserRole] ADD  DEFAULT ((1)) FOR [Active]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__FeedbackD__Feedb__7EC1CEDB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FeedbackDetails] ADD  DEFAULT (getdate()) FOR [FeedbackDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__Status__Active__02925FBF]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  DEFAULT ((1)) FOR [Active]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__Status__PLSource__4460231C]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  DEFAULT ((0)) FOR [PLSource]
END

GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__UserDetai__users__2B947552]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserDetails] ADD  DEFAULT ((2)) FOR [userstatus]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__UserDetai__userC__2C88998B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserDetails] ADD  DEFAULT ((0)) FOR [userCodeType]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessDetails_UserDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessDetails]'))
ALTER TABLE [dbo].[BusinessDetails]  WITH CHECK ADD  CONSTRAINT [FK_BusinessDetails_UserDetails] FOREIGN KEY([userId])
REFERENCES [dbo].[UserDetails] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessDetails_UserDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessDetails]'))
ALTER TABLE [dbo].[BusinessDetails] CHECK CONSTRAINT [FK_BusinessDetails_UserDetails]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessSettings_BusinessDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessSettings]'))
ALTER TABLE [dbo].[BusinessSettings]  WITH CHECK ADD  CONSTRAINT [FK_BusinessSettings_BusinessDetails] FOREIGN KEY([BusinessDetailId])
REFERENCES [dbo].[BusinessDetails] ([businessDetailId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessSettings_BusinessDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessSettings]'))
ALTER TABLE [dbo].[BusinessSettings] CHECK CONSTRAINT [FK_BusinessSettings_BusinessDetails]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessSettings_TreatmentType]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessSettings]'))
ALTER TABLE [dbo].[BusinessSettings]  WITH CHECK ADD  CONSTRAINT [FK_BusinessSettings_TreatmentType] FOREIGN KEY([TreatmentTypeId])
REFERENCES [dbo].[TreatmentType] ([TreatmentTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessSettings_TreatmentType]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessSettings]'))
ALTER TABLE [dbo].[BusinessSettings] CHECK CONSTRAINT [FK_BusinessSettings_TreatmentType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessUserRole_BusinessDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessUserRole]'))
ALTER TABLE [dbo].[BusinessUserRole]  WITH CHECK ADD  CONSTRAINT [FK_BusinessUserRole_BusinessDetails] FOREIGN KEY([BusinessDetailid])
REFERENCES [dbo].[BusinessDetails] ([businessDetailId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessUserRole_BusinessDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessUserRole]'))
ALTER TABLE [dbo].[BusinessUserRole] CHECK CONSTRAINT [FK_BusinessUserRole_BusinessDetails]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessUserRole_BusinessRole]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessUserRole]'))
ALTER TABLE [dbo].[BusinessUserRole]  WITH CHECK ADD  CONSTRAINT [FK_BusinessUserRole_BusinessRole] FOREIGN KEY([BusinessRoleId])
REFERENCES [dbo].[BusinessRole] ([BusinessRoleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessUserRole_BusinessRole]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessUserRole]'))
ALTER TABLE [dbo].[BusinessUserRole] CHECK CONSTRAINT [FK_BusinessUserRole_BusinessRole]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessUserRole_UserDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessUserRole]'))
ALTER TABLE [dbo].[BusinessUserRole]  WITH CHECK ADD  CONSTRAINT [FK_BusinessUserRole_UserDetails] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserDetails] ([UserId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BusinessUserRole_UserDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[BusinessUserRole]'))
ALTER TABLE [dbo].[BusinessUserRole] CHECK CONSTRAINT [FK_BusinessUserRole_UserDetails]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonalDetail_UserSecurityDetails1]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonalDetail]'))
ALTER TABLE [dbo].[PersonalDetail]  WITH CHECK ADD  CONSTRAINT [FK_PersonalDetail_UserSecurityDetails1] FOREIGN KEY([userSecuritydetailId])
REFERENCES [dbo].[UserSecurityDetails] ([UserSecurityDetailId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonalDetail_UserSecurityDetails1]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonalDetail]'))
ALTER TABLE [dbo].[PersonalDetail] CHECK CONSTRAINT [FK_PersonalDetail_UserSecurityDetails1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserDetails_PersonalDetail1]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDetails]'))
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_PersonalDetail1] FOREIGN KEY([PersonalDetailId])
REFERENCES [dbo].[PersonalDetail] ([PersonalDetailId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserDetails_PersonalDetail1]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserDetails]'))
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_PersonalDetail1]
GO
