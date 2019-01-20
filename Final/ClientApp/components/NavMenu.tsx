// Import the stuff we need
import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

// Create a class NavMenu that will contain the data for the navigation menu on the left of the site
export class NavMenu extends React.Component<{}, {}> {
    // The render method: this is what displays the component
    public render() {
        // We return the HTML for the component
        return <div className='main-nav'>
                <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={ '/' }>Workshop 6B</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            // This is where the links are made to different pages, starting with home
                            <NavLink to={ '/' } exact activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>
                        <li>
                            // The link for the students list
                            <NavLink to={ '/students' } activeClassName='active'>
                                <span className='glyphicon glyphicon-education'></span> Students
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
    }
}
