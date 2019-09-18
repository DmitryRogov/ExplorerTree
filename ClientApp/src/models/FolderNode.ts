import NodeBase from './NodeBase'

export default class CatalogNode extends NodeBase {
    constructor(
        public created: string,
        public name: String,
        public path: Number
    ) {
        super(created, name, path);
    }
}