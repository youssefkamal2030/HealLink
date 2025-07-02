import 'package:dots_indicator/dots_indicator.dart';
import 'package:flutter/material.dart';

import '../../../../../core/utils/constant.dart';
import '../../../../../core/utils/function/app_colors.dart';

class CustomDotsIndicator extends StatelessWidget {
  const CustomDotsIndicator({
    super.key,
    required this.index,
  });

  final int index;

  @override
  Widget build(BuildContext context) {
    return Positioned(
      top: AppConstant.height * .805,
      left: 0,
      right: 0,
      child: DotsIndicator(
        dotsCount: 3,
        decorator: DotsDecorator(
          activeColor: AppColors.kPrimaryColor,
          activeSize: Size.square(8),
          size: Size.square(8),
        ),
        position: index.toDouble(),
      ),
    );
  }
}
