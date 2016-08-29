Polymer({
    is: "all-categories",
    properties: {
        categories: {
            type: Array
        }
    },
    ready: function () {
        this.$.requestCategories.generateRequest();
    },
    handleResponse: function (data) {
        this.categories = data.detail.response;
        console.log(this.categories);
    }
})