USE [L1_ManageEmployee]
GO
/****** Object:  Table [dbo].[Commune]    Script Date: 11/5/2024 2:42:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commune](
	[CommuneId] [int] IDENTITY(1,1) NOT NULL,
	[CommuneName] [nvarchar](255) NOT NULL,
	[DistrictId] [int] NOT NULL,
 CONSTRAINT [PK_Commune] PRIMARY KEY CLUSTERED 
(
	[CommuneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Diploma]    Script Date: 11/5/2024 2:42:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diploma](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[IssuedDate] [datetime2](7) NOT NULL,
	[ExpiryDate] [datetime2](7) NULL,
	[EmployeeId] [int] NOT NULL,
	[IssuedByProvinceId] [int] NOT NULL,
 CONSTRAINT [PK_Diploma] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[District]    Script Date: 11/5/2024 2:42:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[District](
	[DistrictId] [int] IDENTITY(1,1) NOT NULL,
	[DistrictName] [nvarchar](255) NOT NULL,
	[ProvinceId] [int] NOT NULL,
 CONSTRAINT [PK_District] PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 11/5/2024 2:42:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[DOB] [datetime2](7) NOT NULL,
	[Age] [int] NOT NULL,
	[Ethnicity] [nvarchar](max) NOT NULL,
	[Job] [nvarchar](max) NOT NULL,
	[CitizenId] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[CommuneId] [int] NULL,
	[DistrictId] [int] NULL,
	[ProvinceId] [int] NULL,
	[AddressDetails] [nvarchar](max) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Province]    Script Date: 11/5/2024 2:42:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Province](
	[ProvinceId] [int] IDENTITY(1,1) NOT NULL,
	[ProvinceName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED 
(
	[ProvinceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Commune] ON 

INSERT [dbo].[Commune] ([CommuneId], [CommuneName], [DistrictId]) VALUES (1, N'Phuc Xa', 1)
INSERT [dbo].[Commune] ([CommuneId], [CommuneName], [DistrictId]) VALUES (2, N'Nguyen Trung Truc', 1)
INSERT [dbo].[Commune] ([CommuneId], [CommuneName], [DistrictId]) VALUES (3, N'Hang Dao', 2)
INSERT [dbo].[Commune] ([CommuneId], [CommuneName], [DistrictId]) VALUES (4, N'Hang Bac', 2)
INSERT [dbo].[Commune] ([CommuneId], [CommuneName], [DistrictId]) VALUES (5, N'Nhat Tan', 3)
INSERT [dbo].[Commune] ([CommuneId], [CommuneName], [DistrictId]) VALUES (6, N'Quang An', 3)
INSERT [dbo].[Commune] ([CommuneId], [CommuneName], [DistrictId]) VALUES (7, N'Ben Nghe', 4)
INSERT [dbo].[Commune] ([CommuneId], [CommuneName], [DistrictId]) VALUES (10, N'Phuong 9', 6)
INSERT [dbo].[Commune] ([CommuneId], [CommuneName], [DistrictId]) VALUES (11, N'Ha Dinh', 7)
INSERT [dbo].[Commune] ([CommuneId], [CommuneName], [DistrictId]) VALUES (14, N'Khuong Ha 1', 7)
SET IDENTITY_INSERT [dbo].[Commune] OFF
GO
SET IDENTITY_INSERT [dbo].[Diploma] ON 

INSERT [dbo].[Diploma] ([Id], [Name], [IssuedDate], [ExpiryDate], [EmployeeId], [IssuedByProvinceId]) VALUES (1, N'Business Administration', CAST(N'2020-02-11T00:00:00.0000000' AS DateTime2), CAST(N'2026-01-01T00:00:00.0000000' AS DateTime2), 3, 1)
INSERT [dbo].[Diploma] ([Id], [Name], [IssuedDate], [ExpiryDate], [EmployeeId], [IssuedByProvinceId]) VALUES (2, N'Information Technology', CAST(N'2000-02-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-09-30T00:00:00.0000000' AS DateTime2), 3, 2)
INSERT [dbo].[Diploma] ([Id], [Name], [IssuedDate], [ExpiryDate], [EmployeeId], [IssuedByProvinceId]) VALUES (7, N'Business Administration', CAST(N'2024-11-11T00:00:00.0000000' AS DateTime2), NULL, 2, 1)
INSERT [dbo].[Diploma] ([Id], [Name], [IssuedDate], [ExpiryDate], [EmployeeId], [IssuedByProvinceId]) VALUES (8, N'Business Administration', CAST(N'2003-11-10T00:00:00.0000000' AS DateTime2), NULL, 1, 2)
INSERT [dbo].[Diploma] ([Id], [Name], [IssuedDate], [ExpiryDate], [EmployeeId], [IssuedByProvinceId]) VALUES (9, N'Business Administration', CAST(N'2003-11-10T00:00:00.0000000' AS DateTime2), NULL, 7, 1)
SET IDENTITY_INSERT [dbo].[Diploma] OFF
GO
SET IDENTITY_INSERT [dbo].[District] ON 

INSERT [dbo].[District] ([DistrictId], [DistrictName], [ProvinceId]) VALUES (1, N'Ba Dinh', 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [ProvinceId]) VALUES (2, N'Hoan Kiem', 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [ProvinceId]) VALUES (3, N'Tay Ho', 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [ProvinceId]) VALUES (4, N'Quan 1', 2)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [ProvinceId]) VALUES (5, N'Quan 3', 2)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [ProvinceId]) VALUES (6, N'Quan 5', 2)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [ProvinceId]) VALUES (7, N'Thanh Xuan', 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [ProvinceId]) VALUES (12, N'Quan 1', 1)
SET IDENTITY_INSERT [dbo].[District] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (1, N'John Doe', CAST(N'1985-05-15T00:00:00.0000000' AS DateTime2), 39, N'Caucasian', N'Software Developer', N'592018374562', N'0182112936', 1, 1, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (2, N'Jane Smith', CAST(N'1990-07-20T00:00:00.0000000' AS DateTime2), 34, N'Asian', N'Project Manager', N'345678901234', N'0123456789', 3, 2, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (3, N'Bob Johnson', CAST(N'1978-03-10T00:00:00.0000000' AS DateTime2), 46, N'African American', N'QA Engineer', N'456789012345', N'0234567890', 2, 1, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (4, N'Alice Brown', CAST(N'1988-11-22T00:00:00.0000000' AS DateTime2), 35, N'Caucasian', N'HR Manager', N'567890123456', N'0345678901', 1, 1, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (5, N'David White', CAST(N'1992-08-14T00:00:00.0000000' AS DateTime2), 32, N'Hispanic', N'Marketing Specialist', N'678901234567', N'0456789012', 1, 1, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (6, N'Eva Green', CAST(N'1983-06-18T00:00:00.0000000' AS DateTime2), 41, N'Asian', N'Financial Analyst', N'789012345678', N'0567890123', 2, 1, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (7, N'Charlie Black', CAST(N'1995-01-05T00:00:00.0000000' AS DateTime2), 29, N'African American', N'UX Designer', N'890123456789', N'0678901234', 3, 2, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (8, N'Grace Lee', CAST(N'1987-09-09T00:00:00.0000000' AS DateTime2), 37, N'Asian', N'Product Manager', N'901234567890', N'0789012345', 4, 2, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (9, N'James Wilson', CAST(N'1980-03-25T00:00:00.0000000' AS DateTime2), 44, N'Caucasian', N'IT Support', N'123456789013', N'0890123456', 3, 2, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (10, N'Isabella Taylor', CAST(N'1994-12-10T00:00:00.0000000' AS DateTime2), 29, N'Hispanic', N'Data Scientist', N'234567890124', N'0901234567', 1, 1, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (11, N'Michael King', CAST(N'1982-07-08T00:00:00.0000000' AS DateTime2), 42, N'African American', N'DevOps Engineer', N'345678901235', N'0123456789', 1, 1, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (12, N'Nina Scott', CAST(N'1993-04-03T00:00:00.0000000' AS DateTime2), 31, N'Caucasian', N'Accountant', N'324423567765', N'0861597531', NULL, NULL, NULL, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (1019, N'Ethan', CAST(N'2003-11-10T00:00:00.0000000' AS DateTime2), 20, N'African American', N'SE', N'567890123457', N'0345678901', 3, 2, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (1021, N'Liam', CAST(N'2003-11-10T00:00:00.0000000' AS DateTime2), 20, N'African American', N'UX/UI', N'678901234568', N'0456789012', 3, 2, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (1022, N'Sophia', CAST(N'2003-11-10T00:00:00.0000000' AS DateTime2), 20, N'African American', N'SE', N'789012345679', N'0567890123', 1, 1, 1, NULL)
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (1023, N'Olivia', CAST(N'2003-11-10T00:00:00.0000000' AS DateTime2), 20, N'African American', N'BA', N'890123456780', N'0678901234', 7, 4, 2, N'12')
INSERT [dbo].[Employees] ([Id], [FullName], [DOB], [Age], [Ethnicity], [Job], [CitizenId], [PhoneNumber], [CommuneId], [DistrictId], [ProvinceId], [AddressDetails]) VALUES (1026, N'John Smith', CAST(N'1987-09-09T00:00:00.0000000' AS DateTime2), 37, N'Asian', N'Product Manager', N'901234567890', N'0789012345', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Province] ON 

INSERT [dbo].[Province] ([ProvinceId], [ProvinceName]) VALUES (1, N'Ha Noi')
INSERT [dbo].[Province] ([ProvinceId], [ProvinceName]) VALUES (2, N'Tp Ho Chi Minh')
SET IDENTITY_INSERT [dbo].[Province] OFF
GO
ALTER TABLE [dbo].[Diploma] ADD  DEFAULT ((0)) FOR [IssuedByProvinceId]
GO
ALTER TABLE [dbo].[Commune]  WITH CHECK ADD  CONSTRAINT [FK_Commune_District_DistrictId] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([DistrictId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Commune] CHECK CONSTRAINT [FK_Commune_District_DistrictId]
GO
ALTER TABLE [dbo].[Diploma]  WITH CHECK ADD  CONSTRAINT [FK_Diploma_Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Diploma] CHECK CONSTRAINT [FK_Diploma_Employees_EmployeeId]
GO
ALTER TABLE [dbo].[Diploma]  WITH CHECK ADD  CONSTRAINT [FK_Diploma_Province_IssuedByProvinceId] FOREIGN KEY([IssuedByProvinceId])
REFERENCES [dbo].[Province] ([ProvinceId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Diploma] CHECK CONSTRAINT [FK_Diploma_Province_IssuedByProvinceId]
GO
ALTER TABLE [dbo].[District]  WITH CHECK ADD  CONSTRAINT [FK_District_Province_ProvinceId] FOREIGN KEY([ProvinceId])
REFERENCES [dbo].[Province] ([ProvinceId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_District_Province_ProvinceId]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Commune_CommuneId] FOREIGN KEY([CommuneId])
REFERENCES [dbo].[Commune] ([CommuneId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Commune_CommuneId]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_District_DistrictId] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[District] ([DistrictId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_District_DistrictId]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Province_ProvinceId] FOREIGN KEY([ProvinceId])
REFERENCES [dbo].[Province] ([ProvinceId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Province_ProvinceId]
GO
