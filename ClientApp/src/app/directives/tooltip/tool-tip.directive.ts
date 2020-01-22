import { Directive, ElementRef, Renderer2, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[toolTip]'
})
export class ToolTipDirective {

  constructor(
    private elem: ElementRef, 
    private renderer: Renderer2) { }

    @Input()
    private text: string;

    private toolTip;

    ngOnInit(): void {
      this.toolTip = this.renderer.createElement('div');
      this.toolTip.innerText = this.text;
      this.renderer.setStyle(this.toolTip, 'background', 'black');
      this.renderer.setStyle(this.toolTip, 'position', 'absolute');
      this.renderer.setStyle(this.toolTip, 'color', 'white');
      this.renderer.setStyle(this.toolTip, 'padding', '15px');
      this.renderer.setStyle(this.toolTip, 'border-radius', '8px');
      this.renderer.setStyle(this.toolTip, 'border', '2px solid white');
      this.renderer.setStyle(this.toolTip, 'visibility', 'hidden');
      let height = this.elem.nativeElement.offsetHeight * 1.5;
      this.renderer.setStyle(this.toolTip, 'top', this.elem.nativeElement.offsetTop - height + 'px');
      this.renderer.setStyle(this.toolTip, 'left', this.elem.nativeElement.offsetLeft + this.elem.nativeElement.offsetWidth + 'px');
      this.renderer.setStyle(this.toolTip, 'z-index', '1');
      const nextElement = this.renderer.nextSibling(this.elem.nativeElement);
      this.renderer.insertBefore(this.elem.nativeElement.parentNode, this.toolTip, nextElement);
    }

    @HostListener('mouseenter')
    onMouseEnter(){
        this.renderer.setStyle(this.toolTip, 'visibility', 'visible');
    }

    @HostListener('mouseleave')
    onMouseLeave(){
      this.renderer.setStyle(this.toolTip, 'visibility', 'hidden');
    }
}
