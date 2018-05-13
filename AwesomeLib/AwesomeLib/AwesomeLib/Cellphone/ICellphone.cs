using AwesomeLib.Cellphone;

namespace AwesomeLib.Eyes
{
    public interface ICellphone
    {
        void ReportResult(Report report);

        void ReportDamage(int[] damageValues);
    }
}
