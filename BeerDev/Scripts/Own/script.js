
var oldNs = oldNs || {};
oldNs.HomePage = function() {
    
}
oldNs.HomePage.prototype = function() {
    var populate = function(beers) {
        var beerBestSeller = $('.beerBestsellers');
        var beerGoodChoices = $('.goodChoices');

        $.each(beers, function (i, data) {
            var productArticle = $('<article class="productArticle"></article>');

            var figure = $('<figure></figure>');
            var thumbnailImage = $('<img class="productArticleThumbnail" />');
            thumbnailImage.attr('src', '/Images/' + data.picture);
            figure.append(thumbnailImage);

            var articleHeader = $('<header></header>');
            var spanHeader = $('<span class="productArticleName"></span>');
            spanHeader.attr('data-beerId', data.beerId);
            spanHeader.text(data.name);
            articleHeader.append(spanHeader);

            var articlePrice = $('<p class="productArticlePrice"></p>');
            articlePrice.text('$' + data.price);

            var addButton = $('<div class="addCartButton"></div>');
            addButton.attr('data-beerName', data.name);
            addButton.attr('data-beerPrice', data.price);
            addButton.text('Add to Cart');

            productArticle.append(figure);
            productArticle.append(articleHeader);
            productArticle.append(articlePrice);
            productArticle.append(addButton);

            if (data.front === 'bestSeller') {
                beerBestSeller.append(productArticle);
            } else if (data.front === 'goodChoice') {
                beerGoodChoices.append(productArticle);
            }
        });
    }
    return { populate: populate }
}();



