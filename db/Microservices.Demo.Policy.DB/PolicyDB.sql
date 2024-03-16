CREATE TABLE [dbo].[Address]
(
	AddressId INT NOT NULL PRIMARY KEY IDENTITY,
	Country VARCHAR(250),
	ZipCode VARCHAR(250),
	City VARCHAR(250),
	Street VARCHAR(250)
)

CREATE TABLE [dbo].[Message]
(
	MessageId INT NOT NULL PRIMARY KEY IDENTITY,
	[Type] VARCHAR(500),
	JsonPayload NVARCHAR(MAX)
)

CREATE TABLE [dbo].[PolicyValidityPeriod]
(
	PolicyValidityPeriodId INT NOT NULL PRIMARY KEY IDENTITY,
	PolicyFrom DATETIME2 NOT NULL,
	PolicyTo DATETIME2 NOT NULL
)

CREATE TABLE [dbo].[OfferStatus]
(
	OfferStatusId INT NOT NULL PRIMARY KEY IDENTITY,
	[Description] VARCHAR(250)
)

CREATE TABLE [dbo].[PolicyHolder]
(
	PolicyHolderId INT NOT NULL PRIMARY KEY IDENTITY,
	FirstName VARCHAR(250),
	LastName VARCHAR(250),
	Pesel VARCHAR(250),
	AddressId INT NOT NULL,

	CONSTRAINT FK_PolicyHolderAddress FOREIGN KEY (AddressId) REFERENCES Address(AddressId),
)

CREATE TABLE [dbo].[Offer]
(
	OfferId INT NOT NULL PRIMARY KEY IDENTITY,
	Number VARCHAR(250),
	ProductCode VARCHAR(250),
	TotalPrice DECIMAL NOT NULL,
	CreationDate DATETIME2 NOT NULL,
	AgentLogin VARCHAR(250),
	PolicyValidityPeriodId INT NOT NULL,
	PolicyHolderId INT NULL,
	OfferStatusId INT NOT NULL,
	
	CONSTRAINT FK_OfferPolicyValidityPeriod FOREIGN KEY (PolicyValidityPeriodId) REFERENCES PolicyValidityPeriod(PolicyValidityPeriodId),
	CONSTRAINT FK_OfferPolicyHolder FOREIGN KEY (PolicyHolderId) REFERENCES PolicyHolder(PolicyHolderId),
	CONSTRAINT FK_OfferOfferStatus FOREIGN KEY (OfferStatusId) REFERENCES OfferStatus(OfferStatusId),
)

CREATE TABLE [dbo].[OfferCover]
(
	OfferCoverId INT NOT NULL PRIMARY KEY IDENTITY,
	OfferId INT NOT NULL,
	Code VARCHAR(250),
	Price DECIMAL NOT NULL,

	CONSTRAINT FK_OfferCoverOffer FOREIGN KEY (OfferId) REFERENCES Offer(OfferId),
)

CREATE TABLE [dbo].[PolicyStatus]
(
	PolicyStatusId INT NOT NULL PRIMARY KEY IDENTITY,
	[Description] VARCHAR(250)
)

CREATE TABLE [dbo].[Policy]
(
	PolicyId INT NOT NULL PRIMARY KEY IDENTITY,
	Number VARCHAR(250),
	ProductCode VARCHAR(250),
	AgentLogin VARCHAR(250),
	PolicyStatusId INT NOT NULL,
	CreationDate DATETIME2,

	CONSTRAINT FK_PolicyPolicyStatus FOREIGN KEY (PolicyStatusId) REFERENCES PolicyStatus(PolicyStatusId),
)

CREATE TABLE [dbo].[PolicyVersion]
(
	PolicyVersionId INT NOT NULL PRIMARY KEY IDENTITY,
	VersionNumber INT NOT NULL,
	TotalPremiumAmount DECIMAL NOT NULL,
	PolicyId INT NOT NULL,
	PolicyHolderId INT NOT NULL,
	CoverPeriodPolicyValidityPeriodId INT NOT NULL,
	VersionValidityPeriodPolicyValidityPeriodId INT NOT NULL,

	CONSTRAINT FK_PolicyVersionPolicy FOREIGN KEY (PolicyId) REFERENCES [Policy](PolicyId),
	CONSTRAINT FK_PolicyVersionPolicyHolder FOREIGN KEY (PolicyHolderId) REFERENCES PolicyHolder(PolicyHolderId),
	CONSTRAINT FK_PolicyVersionCoverPeriodPolicyValidityPeriod FOREIGN KEY (CoverPeriodPolicyValidityPeriodId) REFERENCES PolicyValidityPeriod(PolicyValidityPeriodId),
	CONSTRAINT FK_PolicyVersionVersionValidityPeriodPolicyValidityPeriod FOREIGN KEY (VersionValidityPeriodPolicyValidityPeriodId) REFERENCES PolicyValidityPeriod(PolicyValidityPeriodId),
)

CREATE TABLE [dbo].[PolicyCover]
(
	PolicyCoverId INT NOT NULL PRIMARY KEY IDENTITY,
	Code VARCHAR(250),
	Premium DECIMAL NOT NULL,
	PolicyValidityPeriodId INT NOT NULL,
	PolicyVersionId INT NOT NULL,

	CONSTRAINT FK_PolicyCoverPolicyValidityPeriod FOREIGN KEY (PolicyValidityPeriodId) REFERENCES PolicyValidityPeriod(PolicyValidityPeriodId),
	CONSTRAINT FK_PolicyCoverPolicyVersion FOREIGN KEY (PolicyVersionId) REFERENCES PolicyVersion(PolicyVersionId),
)

INSERT INTO PolicyStatus ([Description]) VALUES ('Active'),('Inactive');
INSERT INTO OfferStatus ([Description]) VALUES ('New'),('Converted'),('Rejected');
