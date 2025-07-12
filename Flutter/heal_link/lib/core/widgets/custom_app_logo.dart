
import 'package:flutter/cupertino.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';

class CustomAppLogo extends StatelessWidget {
  const CustomAppLogo({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(child: Center(child: SvgPicture.asset(AppImages.healLink)));
  }
}