
import 'package:flutter/cupertino.dart';
import 'package:flutter_svg/svg.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class CustomAuthHeader extends StatelessWidget {
  const CustomAuthHeader({super.key, required this.headerTitle, this.onTap});
  final String headerTitle;
  final void Function()? onTap;
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(top: 24),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          GestureDetector(
            onTap: (){
              if (onTap != null) {
                onTap!();
              } else {
                Navigator.pop(context);
              }
            },
            child: SvgPicture.asset(AppImages.arrowLeftIcon),
          ),
          Spacer(),
          Padding(
            padding: const EdgeInsetsDirectional.only(end: 24),
            child: Text(
              headerTitle,
              textAlign: TextAlign.center,
              style: AppTextStyles.popins500style18LightBlackColor.copyWith(
                fontSize: 20,
              ),
            ),
          ),
          Spacer(),
        ],
      ),
    );
  }
}
