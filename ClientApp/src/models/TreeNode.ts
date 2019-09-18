export default abstract class TreeNode<T> {
    constructor(
        public data: T,
        public children: Array<TreeNode<T>>
    ) { }
}
