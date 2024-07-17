CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Address" (
    "ID" uuid NOT NULL,
    "AddressId" text,
    "StreetName" text,
    "StreetNumber" text,
    CONSTRAINT "PK_Address" PRIMARY KEY ("ID")
);

CREATE TABLE "Customer" (
    "TenantId" uuid NOT NULL,
    "CustomerId" text,
    "PartnerId" text,
    "Name" text,
    "AddressID" uuid,
    "CustomerType" integer NOT NULL,
    "CustomerClass" integer NOT NULL,
    "ParentCustomerId" uuid,
    CONSTRAINT "PK_Customer" PRIMARY KEY ("TenantId"),
    CONSTRAINT "FK_Customer_Address_AddressID" FOREIGN KEY ("AddressID") REFERENCES "Address" ("ID"),
    CONSTRAINT "FK_Customer_Customer_ParentCustomerId" FOREIGN KEY ("ParentCustomerId") REFERENCES "Customer" ("TenantId")
);

CREATE INDEX "IX_Customer_AddressID" ON "Customer" ("AddressID");

CREATE INDEX "IX_Customer_ParentCustomerId" ON "Customer" ("ParentCustomerId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240710183249_InitialCreate', '8.0.7');

COMMIT;

