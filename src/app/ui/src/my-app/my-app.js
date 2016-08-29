Polymer({
      is: 'my-app',

      properties: {
        page: {
          type: String,
          reflectToAttribute: true,
          observer: '_pageChanged'
        }
      },

      observers: [
        '_routePageChanged(routeData.page)'
      ],

      _routePageChanged: function(page) {
        this.page = page || 'home';
      },

      _pageChanged: function(page) {
        // Load page import on demand. Show 404 page if fails
        var resolvedPageUrl = this.resolveUrl('../menu-' + page + '/menu-' + page + '.html');
		console.log(resolvedPageUrl);
        this.importHref(resolvedPageUrl, null, this._showPage404, true);
      },

      _showPage404: function() {
        this.page = 'view404';
      }
    });