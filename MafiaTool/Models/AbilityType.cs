using System.ComponentModel;

namespace MafiaTool.Models; 

public enum AbilityType {
    // [Description("Способность убивать других игроков. Выбранная жертва подвергается убийству и, если жертва никем не будет спасена, на следующее утро не проснётся")]
    [Description("Убийство")]
    Kill,
    
    [Description("Лечение")]
    Heal,
    
    [Description("Поиск чёрного")]
    CheckBlack,
    
    // [Description("Способность отменять все эффекты игрока. У выбранного игрока отменяются все эффекты, который были наложены на игрока")]
    [Description("Полная отмена")]
    CancelAll
}