var updateRankingList = function() {
    var list = document.querySelectorAll('#rankingList li');
    var rankingList = nodeListToArray(list);
    for (var index in rankingList) {
        list[index].setAttribute('data-index', index);
    }
}
var dragStart = function (e) {
    var dragData = this.getAttribute('data-index');
    dragData += '-' + this.getAttribute('id');
    e.dataTransfer.setData('Text', dragData);
};

var cancel = function(e) {
    if (e.preventDefault) {
        e.preventDefault();
    }

    if (e.stopPropagation) {
        e.stopPropagation();
    }

    return false;
}

var dropped = function (e) {
    cancel(e);
    var rankingList = document.getElementById('rankingList');
    var dragData = e.dataTransfer.getData('Text').split('-');
    var index = dragData[0];
    var beerId = dragData[1];
    var liDropped = document.getElementById(beerId);
    var targetIndex = e.target.getAttribute('data-index');

    if (index < targetIndex) {
        rankingList.insertBefore(liDropped, e.target);
        rankingList.insertBefore(e.target, liDropped);
    } else {
        rankingList.insertBefore(liDropped, e.target);
    }
    updateRankingList();
}

var loadRanking = function () {
    var beers = new Array();
    var crud = new crudNs.crud('/api/Ranking', 'get');
    crud.sendRequest(function(data) {
        beers = JSON.parse(data);

        var rankingList = document.getElementById('rankingList');
        var fragment = document.createDocumentFragment();

        for (var item = 0; item < beers.length; item++) {
            var beer = beers[item];
            var itemElement = document.createElement('li');
            itemElement.innerHTML = beer.name;
            itemElement.id = beer.beerId;
            itemElement.draggable = true;
            itemElement.setAttribute('data-index', item);
            itemElement.addEventListener('dragstart', dragStart, false);
            itemElement.addEventListener('drop', dropped, false);
            itemElement.addEventListener('dragenter', cancel, false);
            itemElement.addEventListener('dragover', cancel, false);

            fragment.appendChild(itemElement);
        }
        rankingList.appendChild(fragment);
    });
};