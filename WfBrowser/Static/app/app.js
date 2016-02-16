/**
 * Created by Vu on 2016-02-15.
 */
import React from 'react';

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

export default App;

