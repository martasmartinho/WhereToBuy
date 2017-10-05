function invokeButtonClick() {
    $("#btnDummy").trigger('click');
}


function invokeDummyButton(e) {


    if (e === 'Brands') {
        $("#btnBrandDummy").trigger('click');
    }

    if (e === 'Suppliers') {
        //alert("Hello! I am an alert box!!");
        $("#btnSupplierButton").trigger('click');

    }

    if (e === 'States') {
        $("#btnStateDummy").trigger('click');

    }

    if (e === 'Taxes') {
        $("#btnTaxDummy").trigger('click');

    }

    if (e === 'Categories') {
        $("#btnCategoryDummy").trigger('click');

    }

    if (e === 'Supplements') {
        $("#btnSupplementDummy").trigger('click');

    }

    if (e === 'WarningTypes') {
        $("#btnWarningTypeDummy").trigger('click');

    }

    if (e === 'Products') {
        $("#btnProductDummy").trigger('click');

    }
}

function showDropdown(e) {
    $("#" + e).click();
}

function showMessageModel(e) {
    $("#" + e).trigger('click');
    return false;
}

