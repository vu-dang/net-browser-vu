import React from 'react';
import ReactDOM from 'react-dom';

import {Button,Icon,Menu,MenuItem} from 'react-mdl';

var App = React.createClass({
  render: function () {
    return (
      <div>
        <Button id="demo-menu-lower-left">
          <Icon name="layers" />Test Web
        </Button>
        <Menu target="demo-menu-lower-left">
          <MenuItem>Some Action</MenuItem>
          <MenuItem>Another Action</MenuItem>
          <MenuItem disabled>Disabled Action</MenuItem>
          <MenuItem>Yet Another Action</MenuItem>
        </Menu>
      </div>
    );
  }
});

ReactDOM.render(
  <App />, document.getElementById('content')
);

//alert('00:version='+navigator.appVersion + ';mode=' + document.documentMode);
