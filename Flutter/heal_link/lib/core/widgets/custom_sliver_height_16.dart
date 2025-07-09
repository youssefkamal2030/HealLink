
import 'package:flutter/material.dart';

class CustomSliverHeight extends StatelessWidget {
  const CustomSliverHeight(
    this.height , 
    {super.key , 
  
  }
  
  );
final double height ; 
  @override
  Widget build(BuildContext context) {
    return  SliverToBoxAdapter(
      child: SizedBox(
        height: height ,
      ),
    );
  }
}