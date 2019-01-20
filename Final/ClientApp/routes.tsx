// Import all the code that we need
import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { StudentManager } from './components/StudentManager';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Student } from './components/Student';

// Routes are the mappings of the urls with which the user accesses the pages
// A route tells what component to load for what link
export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/students' component={ StudentManager } />
    <Route path='/student/:StudentId' component={ Student } />
</Layout>;
