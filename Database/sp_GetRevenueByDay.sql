-- =====================================================================
-- sp_GetRevenueByDay
-- Mục đích : Tính doanh thu + số hóa đơn theo TỪNG NGÀY trong khoảng
--            [@FromDate, @ToDate] (phục vụ biểu đồ cột doanh thu).
-- Kỹ thuật : GROUP BY CAST(OrderDate AS DATE)
-- =====================================================================
IF OBJECT_ID('dbo.sp_GetRevenueByDay', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetRevenueByDay;
GO

CREATE PROCEDURE dbo.sp_GetRevenueByDay
    @FromDate DATE,
    @ToDate   DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  CAST(o.OrderDate AS DATE)     AS OrderDay,
            COUNT(o.OrderId)              AS TotalOrders,
            ISNULL(SUM(o.TotalAmount), 0) AS TotalRevenue
    FROM    Orders AS o
    WHERE   CAST(o.OrderDate AS DATE) >= @FromDate
      AND   CAST(o.OrderDate AS DATE) <= @ToDate
    GROUP BY CAST(o.OrderDate AS DATE)
    ORDER BY OrderDay;
END
GO
