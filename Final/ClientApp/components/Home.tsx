import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import * as Models from '../Models';

async function loadDefaults(): Promise<string> {
    let jsonResult = await fetch('./api/database/loadDemoData', { method: 'get', credentials: 'include', headers: { 'content-type': 'application/json' } });
    let result = jsonResult.json;
    return `${result}`;
}

export class Home extends React.Component<RouteComponentProps<{}>, { DataStatus: "Defaults have been loaded!" | "Loading..." | "Nothing" }> {
    constructor( props: RouteComponentProps<{}> ) {
        super( props );
        this.state = { DataStatus: "Nothing" };
    }

    async componentDidUpdate() {
        if ( this.state.DataStatus == "Loading...") {
            await loadDefaults().then(
                defaults => this.setState({ ...this.state, DataStatus: "Defaults have been loaded!"})
            );
        }
    }

    doLoadDefaults() {
        this.setState({ ...this.state, DataStatus: "Loading..." });
    }

    public render() {
        return <div>
            <h1>Workshop Development 6B</h1>
            <button onClick={() => this.doLoadDefaults()}>Load Default Values</button>
            <br/><br/>
            <i>
                { this.state.DataStatus == "Nothing" ? "" : this.state.DataStatus }
            </i>
        </div>;
    }
}
