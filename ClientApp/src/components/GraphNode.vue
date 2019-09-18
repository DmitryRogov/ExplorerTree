<template>
  <div
    class="org-tree-node"
    :class="{'is-leaf': !data.children}"
  >
    <div class="org-tree-node-label">
      <div class="org-tree-node-label-inner">
        <div
          :title="data.data.name"
          class="lable-name"
        >
          <svg class="icon">
            <use
              v-if="data.children && data.children.length > 0"
              xlink:href="#folder-1"
            />
            <use
              v-else-if="data.data.size"
              xlink:href="#document"
            />
            <use
              v-else
              xlink:href="#folder"
            />
          </svg>
          {{data.data.name}}</div>
        <div class="node-info">
          <span>{{creationDate}}</span><strong v-if="data.data.size != undefined">&nbsp;{{data.data.size}} Кб</strong>
        </div>
      </div>
    </div>
    <div
      class="org-tree-node-children"
      v-if="data.children"
    >
      <GraphNode
        v-for="child in data.children"
        :key="child.path"
        :data="child"
      ></GraphNode>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component, Prop } from "vue-property-decorator";
import TreeNode from "../models/TreeNode";
import NodeBase from "../models/NodeBase";

@Component
export default class GraphNode extends Vue {
  @Prop({
    type: Object,
    required: true
  })
  data!: TreeNode<NodeBase>;

  private dateFormat: Intl.DateTimeFormatOptions = {
    year: "numeric",
    month: "numeric",
    day: "numeric"
  };

  get creationDate(): String {
    return new Date(this.data.data.created).toLocaleDateString(
      "ru-RU",
      this.dateFormat
    );
  }
}
</script>