import NodeBase from './NodeBase'

export default class FileNode extends NodeBase {
    constructor(
        public created: string,
        public name: String,
        public path: Number,
        public size: Number
    ) {
        super(created, name, path);
    }
}