using cw2.Models;

namespace cw2.Services;

public class PenaltyCalculator
{
    private const decimal DailyPenaltyRate = 10.0m;

    public decimal CalculatePenalty(Rental rental)
    {
        DateTime endDate;
        if (rental.ReturnDate.HasValue)
        {
            endDate = rental.ReturnDate.Value;
        }
        else
        {
            endDate = DateTime.Now;
        }

        if (endDate <= rental.DueDate)
        {
            return 0m;
        }

        var delayDays = (endDate - rental.DueDate).Days;

        if (delayDays > 0)
        {
            return delayDays * DailyPenaltyRate;
        }

        return 0m;
    }
}