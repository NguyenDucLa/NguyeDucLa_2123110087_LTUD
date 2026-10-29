-- =============================================================================
-- Buổi 5 - Tiết 3: Stored Procedure thống kê doanh thu theo nhân viên thu ngân
-- Chạy script này trên CSDL MiniSupermarketDb (SSMS hoặc sqlcmd)
-- =============================================================================
IF OBJECT_ID('sp_GetRevenueByCashier', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetRevenueByCashier;
GO

CREATE PROCEDURE sp_GetRevenueByCashier
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CashierUsername,
        COUNT(OrderId)  AS TotalOrders,
        SUM(TotalAmount) AS TotalRevenue
    FROM Orders
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC;
END
GO
