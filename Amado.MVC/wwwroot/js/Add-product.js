$("#categoryCheckAll").change(function () {
    if ($("#categoryCheckAll").is(":checked")) {
        let categoryChecks = $(".categoryCheck");
        $(".categoryCheck").each(function (key, value) {
            value.checked = true;
        })
    }
    else {
        $(".categoryCheck").each(function (key, value) {
            value.checked = false;
        })
    }
})

$("#brandCheckAll").change(function () {
    if ($("#brandCheckAll").is(":checked")) {
        let categoryChecks = $(".brandCheck");
        $(".brandCheck").each(function (key, value) {
            value.checked = true;
        })
        GetAndSendValues();
    }
    else {
        $(".brandCheck").each(function (key, value) {
            value.checked = false;
        })
        GetAndSendValues();
    }
})


$(".filterInput").change(function () {
    GetAndSendValues();
});

const GetAndSendValues = () => {
    let categoryChecks = $(".categoryCheck");
    let categoryIds = [];
    let brandIds = [];
    let sortingOption = $("#sortByPrice").val();
    for (var i = 0; i < categoryChecks.length; i++) {
        if (categoryChecks[i].checked) {
            categoryIds.push(categoryChecks[i].value)
        }
    }
    let brandChecks = $(".brandCheck");
    for (var i = 0; i < brandChecks.length; i++) {
        if (brandChecks[i].checked) {
            brandIds.push(brandChecks[i].value)
        }
    }
    SendPostRequest(categoryIds, brandIds, sortingOption);
}


const Handle = (list) => {
    $("#productRow").html("");
    for (var i = 0; i < list.length; i++) {
        console.log(list);
        $("#productRow").prepend(
            `
                <div class="col-12 col-sm-6 col-md-12 col-xl-6">
                    <div class="single-product-wrapper">
                        <!-- Product Image -->
                        <div class="product-img">
                            <img src="${list[i].productImages[0].url}" alt="${list[0].productName}_Image">
                            <!-- Hover Thumb -->
                            <img class="hover-img" src="${list[i].productImages[1].url}" alt="${list[i].productName}_Image2">
                        </div>

                        <!-- Product Description -->
                        <div class="product-description d-flex align-items-center justify-content-between">
                            <!-- Product Meta Data -->
                            <div class="product-meta-data">
                                <div class="line"></div>
                                <p class="product-price">${list[i].price}</p>
                                <h6>${list[i].productName}</h6>
                            </div>
                            <!-- Ratings & Cart -->
                            <div class="ratings-cart text-right">
                                <div class="ratings">
                                    <i class="fa fa-star" aria-hidden="true"></i>
                                    <i class="fa fa-star" aria-hidden="true"></i>
                                    <i class="fa fa-star" aria-hidden="true"></i>
                                    <i class="fa fa-star" aria-hidden="true"></i>
                                    <i class="fa fa-star" aria-hidden="true"></i>
                                </div>
                                <div class="cart">
                                    <a href="cart.html" data-toggle="tooltip" data-placement="left" title="Sepete Ekle"><img src="img/core-img/cart.png" alt=""></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                `
        );
    }
};

const SendPostRequest = (categoryIds, brandIds, sortingOption) => {

    let postData = {
        Categories: [...categoryIds],
        Brands: [...brandIds],
        SortingOption: sortingOption
    };

    console.log(postData);
    $.ajax({
        url: "/Product/GetProductbyFilter",
        type: "POST",
        async: false,
        dataType: "JSON",
        data: postData,
        success: function (result) {
            Handle(result);
        }
    })
}

