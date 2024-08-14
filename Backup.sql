-- Adminer 4.8.1 PostgreSQL 16.4 (Debian 16.4-1.pgdg120+1) dump

\connect "OrderDB";

DROP TABLE IF EXISTS "OrderProduct";
DROP SEQUENCE IF EXISTS "OrderProduct_Id_seq";
CREATE SEQUENCE "OrderProduct_Id_seq" INCREMENT  MINVALUE  MAXVALUE  CACHE ;

CREATE TABLE "public"."OrderProduct" (
    "OrderProductId" integer DEFAULT nextval('"OrderProduct_Id_seq"') NOT NULL,
    "ProductId" integer NOT NULL,
    "OrderId" integer NOT NULL,
    "CreatedDate" date NOT NULL,
    "UpdateDate" date NOT NULL,
    "Count" integer NOT NULL,
    "Reserved" boolean NOT NULL,
    CONSTRAINT "OrderProduct_pkey" PRIMARY KEY ("OrderProductId")
) WITH (oids = false);


DROP TABLE IF EXISTS "Orders";
DROP SEQUENCE IF EXISTS "Orders_OrderId_seq";
CREATE SEQUENCE "Orders_OrderId_seq" INCREMENT  MINVALUE  MAXVALUE  CACHE ;

CREATE TABLE "public"."Orders" (
    "OrderId" integer DEFAULT nextval('"Orders_OrderId_seq"') NOT NULL,
    "CreatedDate" date NOT NULL,
    "UpdateDate" date NOT NULL,
    "CustomerId" integer NOT NULL,
    CONSTRAINT "Orders_pkey" PRIMARY KEY ("OrderId")
) WITH (oids = false);


\connect "ProductDB";

DROP TABLE IF EXISTS "Products";
DROP SEQUENCE IF EXISTS "Products_ProductId_seq";
CREATE SEQUENCE "Products_ProductId_seq" INCREMENT  MINVALUE  MAXVALUE  CACHE ;

CREATE TABLE "public"."Products" (
    "ProductId" integer DEFAULT nextval('"Products_ProductId_seq"') NOT NULL,
    "Name" text NOT NULL,
    "Description" text,
    "Price" money NOT NULL,
    CONSTRAINT "Products_pkey" PRIMARY KEY ("ProductId")
) WITH (oids = false);

INSERT INTO "Products" ("ProductId", "Name", "Description", "Price") VALUES
(1,	'Bilgisayar',	'Teknolojik Alet',	'$20,000.00'),
(2,	'Telefon',	'Teknolojik Alet',	'$15,000.00'),
(3,	'Koltuk',	'Mobilya',	'$9,000.00');

\connect "StockDB";

DROP TABLE IF EXISTS "Stocks";
DROP SEQUENCE IF EXISTS "Stocks_StockId_seq";
CREATE SEQUENCE "Stocks_StockId_seq" INCREMENT  MINVALUE  MAXVALUE  CACHE ;

CREATE TABLE "public"."Stocks" (
    "StockId" integer DEFAULT nextval('"Stocks_StockId_seq"') NOT NULL,
    "ProductId" integer NOT NULL,
    "TotalCount" integer NOT NULL,
    "ReservedCount" integer NOT NULL,
    "CreatedDate" date NOT NULL,
    "UpdatedDate" date NOT NULL,
    CONSTRAINT "Stocks_pkey" PRIMARY KEY ("StockId")
) WITH (oids = false);


-- 2024-08-13 19:45:51.682691+00