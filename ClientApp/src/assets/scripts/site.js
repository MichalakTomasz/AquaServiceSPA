$(document).ready(() => {
    $('.info-express').popover({
        content: 'kalkulator do sporządzania nawozów każdego w osobnym pojemniku. Po podaniu dwóch parametrów: wielkości akwarum i pojemności pojemnika na nawóz, kalkulator uwzględniając zalecane proporcje nawozów, obliczy ile gram danej soli należy wsypać do danego pojemnika z wodą demineralizowaną. Wynikiem będzie ile miligramów na litr danego składnika będzie zawierał jeden mililitr naszego nawozu.',
        placement: "right",
        animation: true,
    });

    $('.info-macro').popover({
        content: '"Kalkulator Makro" służy do sporządzenia nawozu z wymaganycy soli mineralnych w jednym pojemniku. W miejscu "Parametry do sporządzenia nawozu" podajemy: ile litrów wody (bez podłoża, kamieni, korzeni, roślin, ect.) mamy w akwarium. Następnie podajemy w jakiej ilości wody będziemy mieszać nawóz oraz ile razy w tygodniu chcemy podawać nawóz. Następnie podajemy jakie stężenia pierwiastków (azotu, fosforu, potasu, magnezu) chcemy uzyskać po tygodniowym dawkowaniu nawozu. Jeżeli mamy zamiar zrobić nawóz bez jakiegoś składnika w jego mijscu wpisujemy 0. Kalkulator oblicza ile gram każdej soli musimy dodać do ustalonej wcześniej ilości wody demineralizowanej i ilość mililitrów jednorazowej dawki nawozu.',
        placement: "right",
        animation: true,
    });

    $('.info-salt').popover({
        content: 'Kalkulator soli mineralnych służy jako pomoc do sporządzenia osobnych nawozów poszczególnych składników makroelementowych. Po uzupełnieniu pól: ilość litrów wody w akwarium, ilości soli jaką dodamy do danej ilości wody demineralizowanej do stworzenia nawozu, otrzymamy jakie stężenie zawieranych pierwiastków chemicznych będzie miał jeden mililitr nawozu. Dane stężeń i rozpuszczlności soli pobierane są z tabel powyżej.',
        placement: "right",
        animation: true,
    });

    const aquaLiters = 'Pojemność akwarium netto (bez żwiru, korzeni, roślin ect.)';
    const containerCapacity = 'Pojemność zbiornika w którym będzie sporządzany nawóz.';
    const saltGrams = 'Ile gram soli chcesz wsypać?'

    $('.aqua-liters').tooltip({
        title: aquaLiters,
        animation: true
    });

    $('.container-capacity').tooltip({
        title: containerCapacity,
        animation: true
    });

    $('#K2SO4_K2SO4g').tooltip({
        title: saltGrams,
        animation: true
    });

    $('#KNO3_KNO3g').tooltip({
        title: saltGrams,
        animation: true
    });

    $('#KH2PO4_ContainerCapacity').tooltip({
        title: containerCapacity,
        animation: true
    });

    $('#KH2PO4_KH2PO4g').tooltip({
        title: saltGrams,
        animation: true
    });

    $('#MgSO4_ContainerCapacity').tooltip({
        title: containerCapacity,
        animation: true
    });

    $('#MgSO4_MgSO4g').tooltip({
        title: saltGrams,
        animation: true
    });

    $('#Macro_TimesAWeak').tooltip({
        title: 'Ile razy w tygodniu chcesz podawać nawóz?',
        animation: true
    });

    $('#Macro_Nitrogen').tooltip({
        title: 'Jakie stężenie azotu chcesz uzyskać po tygodniowym dawkowaniu nawozu?',
        animation: true
    });

    $('#Macro_Phosphorus').tooltip({
        title: 'Jakie stężenie fosforu chesz uzyskać po tygodniowym dawkowaniu nawozu?',
        animation: true
    });

    $('#Macro_Potassium').tooltip({
        title: 'Jakie stężenie potasu chcesz uzyskać po tygodniowym dawkowaniu nawozu?',
        animation: true
    });

    $('#Macro_Magnesium').tooltip({
        title: 'Jakie stężenie magnezu chcesz uzyskać po tygodniowym dawkowaniu nawozu?',
        animation: true
    });
    //$('.first-input').focus();
});
