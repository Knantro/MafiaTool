namespace MafiaTool.ViewModels;

public class MafiaGameVM : ViewModelBase {
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    public MafiaGameVM() {
        logger.SignedDebug("ctor");
    }
}