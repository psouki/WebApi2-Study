
var detailNS = detailNS || {};

detailNS.DetailPage = function () {
    this.beer = undefined;
    this.beerFound = false;
}
detailNS.DetailPage.prototype = function () {
    var build = function(beer) {
            var header = document.querySelector('.blockHeaderBeer span');
            if (beer === undefined || beer === null) {
                header.innerHTML = 'Beer not found"';
            } else {
                var image = document.getElementsByClassName('productArticleLargeImage');
                var nationality = document.querySelector('div.info div:first-child .back');
                var category = document.querySelector('div.info div:nth-child(2) .back');
                var alchool = document.querySelector('div.info div:nth-child(3) .back');
                var description = document.querySelector('div.productArticleContent p.productArticleDescription');
                var price = document.querySelector('div.productArticleContent p.productArticlePrice');

                header.innerHTML = beer.name;

                var searchPic = new Image(100, 100);
                searchPic.src = '/Images/' + beer.picture;
                image[0].src = searchPic.src;
                image[0].width = 462;
                image[0].height = 515;

                nationality.innerHTML = beer.nationality;
                category.innerHTML = beer.kind;
                alchool.innerHTML = beer.alchool;
                description.innerHTML = beer.description;
                price.innerHTML = 'Price: $' + beer.price;
            }
        }
    return { build: build };
}();

var loadPage = function () {
    var beerClicked = 'nda';
    if (localStorage.getItem('beerDetails')) {
        beerClicked = localStorage.getItem('beerDetails');
    }

    var appPath = 'http://localhost:11390/api/beers/' + beerClicked;

    var crud = new crudNs.crud(appPath, 'get');
    crud.sendRequest(function(data) {
        var page = new detailNS.DetailPage();
        page.build(data);
    });
    
};
